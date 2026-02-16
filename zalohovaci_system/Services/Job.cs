using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;

namespace zalohovaci_system.Services
{
    public abstract class Job
    {
        public virtual void Backup(BackupJob job, List<Snapshot> snapshots)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"));

            CheckRetention(job, snapshots);

            foreach (string source in job.Sources)
            {
                foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
                {
                    string relativePath = Path.GetRelativePath(source, file);
                    string targetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp", Path.GetFileName(source), relativePath);
                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                    File.Copy(file, targetPath, true);
                }
            }

            if (Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), "*", SearchOption.AllDirectories).Length == 0)
            {
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), true);
                throw new Exception("Nothing to backup.");
            }

            foreach (string target in job.Targets)
            {
                string zipPath = Path.Combine(target, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
                ZipFile.CreateFromDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), zipPath);
            }

            CreateSnapshot(snapshots, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), job.Method, job.Id);

            Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), true);
        }

        public virtual void CreateSnapshot(List<Snapshot> snapshots, string path, BackupMethod method, int id)
        {
            snapshots.Add(new Snapshot
            {
                Id = snapshots.Count == 0 ? 0 : snapshots.Max(s => s.Id) + 1,
                CreatedAt = DateTime.Now,
                MethodType = method.ToString()
            });

            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper","snapshots"));

            using (StreamWriter file = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper","snapshots", $"snapshots_id{id}.json")))
            {
                file.Write(JsonConvert.SerializeObject(snapshots));
                file.Flush();
            }

            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Backup job (ID: {id}) completed using method: {method}.");
        }

        public virtual bool CheckRetention(BackupJob job, List<Snapshot> snapshots)
        {
            if (snapshots.Count / job.Retention.Size >= job.Retention.Count)
            {
                List<Snapshot> oldestSnapshots = snapshots.OrderBy(x => x.CreatedAt).ToList();
                for (int i = 0; i < job.Retention.Size; i++)
                {
                    snapshots.Remove(oldestSnapshots[i]);
                }
                
                foreach (string target in job.Targets)
                {
                    string oldestBackup = Path.GetFileName(Directory.GetFiles(target).OrderBy(x => File.GetCreationTime(x)).First());
                    File.Delete(target + "\\" + oldestBackup);
                }
            }

            if (snapshots.Count >= job.Retention.Size)
            {
                for (int i = 0; i < job.Retention.Size - 1; i++)
                {
                    if (snapshots[snapshots.Count - 1 - i].MethodType == "Full") return false;
                }

                return true;
            }
            
            return false;
        }
    }
}
