using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;

namespace zalohovaci_system.Services
{
    internal class JobFULL : Job
    {
        public override void CreateSnapshot(List<Snapshot> snapshots, string path, BackupMethod method, int id)
        {
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Backup job (ID: {id}) completed using method: {method}.");
        }

        public override bool CheckRetention(BackupJob job, List<Snapshot> snapshots)
        {
            if (Directory.GetFiles(job.Targets[0]).Length >= job.Retention.Count)
            {
                string oldestBackup = Path.GetFileName(Directory.GetFiles(job.Targets[0]).OrderBy(x => File.GetCreationTime(x)).First());
                foreach (string target in job.Targets)
                {
                    File.Delete(target + "\\" + oldestBackup);
                }
            }
            return false;
        }
    }
}
