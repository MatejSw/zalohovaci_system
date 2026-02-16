using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;

namespace zalohovaci_system.Services
{
    public class JobDIFF : Job
    {
        public override void Backup(BackupJob job, List<Snapshot> snapshots)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"));

            bool fullBackup = CheckRetention(job, snapshots);

            DateTime snapshot = snapshots.Where(x => x.MethodType == "Full").OrderByDescending(s => s.CreatedAt).FirstOrDefault()?.CreatedAt ?? DateTime.MinValue;

            foreach (string source in job.Sources)
            {
                foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
                {
                    if (File.GetLastWriteTime(file) <= snapshot && Directory.GetFiles(source, "*", SearchOption.AllDirectories).Length != 0 && !fullBackup) continue;

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
                Directory.CreateDirectory(Path.Combine(target, $"current_package"));

                int y = Directory.GetDirectories(Path.Combine(target, $"current_package"), "*", SearchOption.TopDirectoryOnly).Length;
                if (Directory.GetDirectories(Path.Combine(target, $"current_package"), "*", SearchOption.TopDirectoryOnly).Length == job.Retention.Size)
                {
                    ZipFile.CreateFromDirectory(Path.Combine(target, $"current_package"), Path.Combine(target, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip"));
                    Directory.Delete(Path.Combine(target, "current_package"), true);
                    Directory.CreateDirectory(Path.Combine(target, $"current_package"));
                }
                if (fullBackup || snapshots.Count == 0)
                {
                    ZipFile.CreateFromDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), Path.Combine(target, "current_package", "full.zip"));
                    ZipFile.ExtractToDirectory(Path.Combine(target, "current_package", "full.zip"), Path.Combine(target, "current_package", "full"));
                    File.Delete(Path.Combine(target, "current_package", "full.zip"));
                }

                else
                {
                    int x = Directory.GetDirectories(Path.Combine(target, $"current_package"), "*", SearchOption.TopDirectoryOnly).Length - 1;
                    ZipFile.CreateFromDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), Path.Combine(target, "current_package", $"diff_{x}.zip"));
                    ZipFile.ExtractToDirectory(Path.Combine(target, "current_package", $"diff_{x}.zip"), Path.Combine(target, "current_package", $"diff_{x}"));
                    File.Delete(Path.Combine(target, "current_package", $"diff_{x}.zip"));
                }
            }

            if (snapshots.Count == 0 || fullBackup) CreateSnapshot(snapshots, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), BackupMethod.Full, job.Id);
            else CreateSnapshot(snapshots, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), job.Method, job.Id);

            Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), true);
        }
    }
}
