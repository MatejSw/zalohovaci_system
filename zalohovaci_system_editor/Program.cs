using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using zalohovaci_system.Model;
using zalohovaci_system_api;
using zalohovaci_system_api.Models;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Services;
using zalohovaci_system_editor.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace zalohovaci_system_editor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Zálohovací systém - Editor";

            MyContext context = new MyContext();

            List<zalohovaci_system.Model.BackupJob> backupJobs = new();

            for (int i = 0; i < context.BackupJobs.Count(); i++)
            {
                zalohovaci_system_api.Models.BackupJob backupJob = context.BackupJobs.ToList()[i];

                zalohovaci_system.Model.BackupMethod method = zalohovaci_system.Model.BackupMethod.Full;
                
                switch (context.BackupMethod.Find(backupJob.method).id)
                {
                    case 1:
                        method = zalohovaci_system.Model.BackupMethod.Full;
                        break;
                    case 2:
                        method = zalohovaci_system.Model.BackupMethod.Differential;
                        break;
                    case 3:
                        method = zalohovaci_system.Model.BackupMethod.Incremental;
                        break;

                }

                zalohovaci_system.Model.BackupRetention retention = new()
                {
                    Size = context.BackupRetention.Find(backupJob.retention).size,
                    Count = context.BackupRetention.Find(backupJob.retention).count
                };
                List<int> sourcesIds = context.PathToJobs.Where(x => x.jobId == backupJob.Id && x.pathType == 1).Select(x => x.pathId).ToList();
                List<int> targetsIds = context.PathToJobs.Where(x => x.jobId == backupJob.Id && x.pathType == 2).Select(x => x.pathId).ToList();
                List<string> sources = context.FilePaths.Where(x => sourcesIds.Contains(x.id)).Select(x => x.filepath).ToList();
                List<string> targets = context.FilePaths.Where(x => targetsIds.Contains(x.id)).Select(x => x.filepath).ToList();
                zalohovaci_system.Model.BackupJob backup = new()
                {
                    Id = backupJob.jobId,
                    Sources = sources,
                    Targets = targets,
                    Retention = retention,
                    Method = method,
                    Timing = backupJob.timing
                };

                backupJobs.Add(backup);
            }

            ConfigListWindow configListWindow = new ConfigListWindow(backupJobs);
            EditorWindow editorWindow = new EditorWindow();
            EmptyWindow emptyWindow = new EmptyWindow();
            CreateNewJobDBox createNewJobDBox = new CreateNewJobDBox();
            CreateNewJobWindow createNewJobWindow = new CreateNewJobWindow();
            DeleteJobDBox deleteJobDBox = new DeleteJobDBox();

            SinglePane singlePane = new SinglePane();
            DoublePane doublePane = new DoublePane()
            {
                LeftWindow = configListWindow,
                RightWindow = emptyWindow
            };
            DialogueBox dialogueBox = new DialogueBox();

            UI uI = new UI();
            uI.modules.Push(doublePane);

            configListWindow.OnConfigSelected += (backupJob) =>
            {
                editorWindow.LoadBackupJob(backupJob);
                doublePane.RightWindow = editorWindow;
                editorWindow.SelectWindow();
                doublePane.ActiveSide = true;
            };

            configListWindow.CreateNewJob += () =>
            {
                dialogueBox.Window = createNewJobDBox;
                createNewJobDBox.SelectWindow();
                uI.modules.Push(dialogueBox);
            };

            configListWindow.DeleteSelectedJob += (backupJob) =>
            {
                string id = backupJob.Id;
                dialogueBox.Window = deleteJobDBox;
                deleteJobDBox.Id = id;
                uI.modules.Push(dialogueBox);
            };

            editorWindow.Cancel += () =>
            {
                doublePane.RightWindow = emptyWindow;
                doublePane.ActiveSide = false;
            };

            editorWindow.SaveChanges += () =>
            {
                configListWindow.SaveBackup(editorWindow.SelectedBackupJob);
                SaveChanges(backupJobs);
            };

            editorWindow.EditDirectories += (list) =>
            {
                DirectorySelectorWindow tempDSW = new DirectorySelectorWindow();
                DirectoryListWindow tempDLW = new DirectoryListWindow();
                DoublePane doublePane2 = new DoublePane()
                {
                    LeftWindow = tempDSW,
                    RightWindow = tempDLW
                };

                uI.modules.Push(doublePane2);

                tempDLW.SetDirectories(list);
                tempDSW.SelectWindow();

                tempDSW.OnDirectorySelected += (directory) =>
                {
                    tempDLW.AddDirectory(directory);
                };

                tempDSW.SwitchWindow += () =>
                {
                    doublePane2.ActiveSide = true;
                    tempDLW.SelectWindow();
                };

                tempDSW.ChangeDriveEvent += () =>
                {
                    ChangeDriveWindow changeDriveWindow = new ChangeDriveWindow();
                    dialogueBox.Window = changeDriveWindow;
                    uI.modules.Push(dialogueBox);
                    changeDriveWindow.Confirm += (drive) =>
                    {
                        tempDSW.ChangeDrive(drive);
                        uI.modules.Pop();
                    };
                    changeDriveWindow.Cancel += () =>
                    {
                        uI.modules.Pop();
                    };
                };

                tempDLW.SwitchWindow += () =>
                {
                    doublePane2.ActiveSide = false;
                    tempDSW.SelectWindow();
                };

                tempDLW.Cancel += () =>
                {
                    uI.modules.Pop();
                };

                tempDLW.SaveDirectories += (directories) =>
                {
                    editorWindow.SaveDirectories(directories);
                    uI.modules.Pop();
                };
            };

            createNewJobDBox.Cancel += () =>
            {
                uI.modules.Pop();
            };

            createNewJobDBox.Create += (id) =>
            {
                singlePane.ParentWindow = createNewJobWindow;
                zalohovaci_system.Model.BackupJob job = new zalohovaci_system.Model.BackupJob()
                {
                    Id = id,
                    Sources = new List<string>(),
                    Targets = new List<string>(),
                    Timing = "* * * * *",
                    Method = zalohovaci_system.Model.BackupMethod.Full,
                    Retention = new zalohovaci_system.Model.BackupRetention()
                    {
                        Size = 0,
                        Count = 0
                    },
                };
                createNewJobWindow.LoadBackupJob(job);
                uI.modules.Push(singlePane);
            };

            createNewJobWindow.Cancel += () =>
            {
                uI.modules.Pop();
                uI.modules.Pop();
            };

            createNewJobWindow.Create += (id) =>
            {
                zalohovaci_system.Model.BackupJob job = createNewJobWindow.SelectedBackupJob;
                configListWindow.AddBackup(job);
                AddJob(job);
                uI.modules.Pop();
                uI.modules.Pop();
            };

            createNewJobWindow.EditDirectories += (list) =>
            {
                DirectorySelectorWindow tempDSW = new DirectorySelectorWindow();
                DirectoryListWindow tempDLW = new DirectoryListWindow();
                DoublePane doublePane2 = new DoublePane()
                {
                    LeftWindow = tempDSW,
                    RightWindow = tempDLW
                };

                uI.modules.Push(doublePane2);

                tempDLW.SetDirectories(list);
                tempDSW.SelectWindow();

                tempDSW.OnDirectorySelected += (directory) =>
                {
                    tempDLW.AddDirectory(directory);
                };

                tempDSW.SwitchWindow += () =>
                {
                    doublePane2.ActiveSide = true;
                    tempDLW.SelectWindow();
                };

                tempDSW.ChangeDriveEvent += () =>
                {
                    ChangeDriveWindow changeDriveWindow = new ChangeDriveWindow();
                    dialogueBox.Window = changeDriveWindow;
                    uI.modules.Push(dialogueBox);
                    changeDriveWindow.Confirm += (drive) =>
                    {
                        tempDSW.ChangeDrive(drive);
                        uI.modules.Pop();
                    };
                    changeDriveWindow.Cancel += () =>
                    {
                        uI.modules.Pop();
                    };
                };

                tempDLW.SwitchWindow += () =>
                {
                    doublePane2.ActiveSide = false;
                    tempDSW.SelectWindow();
                };

                tempDLW.Cancel += () =>
                {
                    uI.modules.Pop();
                };

                tempDLW.SaveDirectories += (directories) =>
                {
                    createNewJobWindow.SaveDirectories(directories);
                    uI.modules.Pop();
                };
            };

            deleteJobDBox.Cancel += () =>
            {
                uI.modules.Pop();
            };

            deleteJobDBox.Delete += (id) =>
            {
                zalohovaci_system.Model.BackupJob? job = backupJobs.FirstOrDefault(j => j.Id == id);
                if (job != null)
                {
                    configListWindow.RemoveBackup(job);
                    backupJobs.Remove(job);
                    RemoveJob(job);
                }
                uI.modules.Pop();
            };

            while (true)
            {
                uI.Draw();
                ConsoleKeyInfo key = Console.ReadKey(true);
                uI.HandleKey(key);
            }
        }

        public static void SaveChanges(List<zalohovaci_system.Model.BackupJob> list)
        {
            MyContext context = new MyContext();

            for (int i = 0; i < list.Count; i++)
            {
                zalohovaci_system_api.Models.BackupJob db = context.BackupJobs.ToList()[i] ?? new();
                zalohovaci_system.Model.BackupJob job = list[i];

                zalohovaci_system_api.Models.BackupRetention retention = new()
                {
                    count = job.Retention.Count,
                    size = job.Retention.Size
                };
                bool newRetention = true;

                foreach (zalohovaci_system_api.Models.BackupRetention item in context.BackupRetention)
                {
                    if (job.Retention.Count == item.count && job.Retention.Size == item.size)
                    {
                        retention = item;
                        newRetention = false;
                        break;
                    }
                }

                if (newRetention)
                {
                    context.BackupRetention.Add(retention);
                    context.SaveChanges();
                }

                zalohovaci_system_api.Models.BackupMethod method = new()
                {
                    methodName = job.Method.ToString()
                };

                foreach (zalohovaci_system_api.Models.BackupMethod item in context.BackupMethod)
                {
                    if (item.methodName.ToLower() == method.methodName.ToLower()) method = item;
                }

                db.method = method.id;
                db.retention = retention.id;
                db.timing = job.Timing;
                db.jobId = job.Id;
            }

            context.SaveChanges();
        }

        public static void AddJob(zalohovaci_system.Model.BackupJob job)
        {
            MyContext context = new MyContext();
            zalohovaci_system_api.Models.BackupJob db = new();

            zalohovaci_system_api.Models.BackupRetention retention = new()
            {
                count = job.Retention.Count,
                size = job.Retention.Size
            };
            bool newRetention = true;

            foreach (zalohovaci_system_api.Models.BackupRetention item in context.BackupRetention)
            {
                if (job.Retention.Count == item.count && job.Retention.Size == item.size)
                {
                    retention = item;
                    newRetention = false;
                    break;
                }
            }

            if (newRetention)
            {
                context.BackupRetention.Add(retention);
                context.SaveChanges();
            }

            zalohovaci_system_api.Models.BackupMethod method = new()
            {
                methodName = job.Method.ToString()
            };

            foreach (zalohovaci_system_api.Models.BackupMethod item in context.BackupMethod)
            {
                if (item.methodName.ToLower() == method.methodName.ToLower()) method = item;
            }

            db.method = method.id;
            db.retention = retention.id;
            db.timing = job.Timing;
            db.jobId = job.Id;
            db.createdAt = DateTime.Now;

            context.BackupJobs.Add(db);
            context.SaveChanges();
        }

        public static void RemoveJob(zalohovaci_system.Model.BackupJob job)
        {
            MyContext context = new MyContext();
            zalohovaci_system_api.Models.BackupJob jobToRemove = new();

            foreach (zalohovaci_system_api.Models.BackupJob item in context.BackupJobs)
            {
                if (job.Id == item.jobId)
                {
                    jobToRemove = item;
                }
            }

            context.BackupJobs.Remove(jobToRemove);
            context.SaveChanges();
        }
    }
}
