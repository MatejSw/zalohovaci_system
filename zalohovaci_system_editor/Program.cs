using Newtonsoft.Json;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Services;
using zalohovaci_system_editor.Windows;

namespace zalohovaci_system_editor
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Zálohovací systém - Editor";

            List<BackupJob> backupJobs = JsonConvert.DeserializeObject<List<BackupJob>>(File.ReadAllText(@"C:\Users\matej\source\repos\zalohovaci_system\zalohovaci_system\conf\backup_config.json")) ?? new List<BackupJob>();

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
                using (StreamWriter sw = new StreamWriter(@"C:\Users\matej\source\repos\zalohovaci_system\zalohovaci_system\conf\backup_config.json"))
                {
                    sw.Write(JsonConvert.SerializeObject(backupJobs, Formatting.Indented));
                }
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
                BackupJob job = new BackupJob()
                {
                    Id = id,
                    Sources = new List<string>(),
                    Targets = new List<string>(),
                    Timing = "* * * * *",
                    Method = BackupMethod.Full,
                    Retention = new BackupRetention()
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
                BackupJob job = createNewJobWindow.SelectedBackupJob;
                configListWindow.AddBackup(job);
                using (StreamWriter sw = new StreamWriter(@"C:\Users\matej\source\repos\zalohovaci_system\zalohovaci_system\conf\backup_config.json"))
                {
                    sw.Write(JsonConvert.SerializeObject(backupJobs, Formatting.Indented));
                }
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
                BackupJob? job = backupJobs.FirstOrDefault(j => j.Id == id);
                if (job != null)
                {
                    configListWindow.RemoveBackup(job);
                    backupJobs.Remove(job);
                    using (StreamWriter sw = new StreamWriter(@"C:\Users\matej\source\repos\zalohovaci_system\zalohovaci_system\conf\backup_config.json"))
                    {
                        sw.Write(JsonConvert.SerializeObject(backupJobs, Formatting.Indented));
                    }
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
    }
}
