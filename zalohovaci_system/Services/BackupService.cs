using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;

namespace zalohovaci_system.Services
{
    public class BackupService
    {
        private JobFULL jobFull;
        private JobDIFF jobDiff;
        private JobINCR jobIncr;
        private CronHandler cronHandler;

        public BackupService() 
        {
            jobFull = new JobFULL();
            jobDiff = new JobDIFF();
            jobIncr = new JobINCR();
            cronHandler = new CronHandler();
        }

        public async Task RunBackupJobs(List<BackupJob> backupJobs)
        {
            foreach (BackupJob job in backupJobs)
            {
                job.NextOccurence = (DateTime)cronHandler.GetNextOccurrence(job.Timing, DateTime.UtcNow);
            }
            List<Snapshot> Snapshots;
            while (true)
            {
                foreach (var job in backupJobs)
                {
                    if (job.NextOccurence >= DateTime.UtcNow) continue;
                    if (job == null) continue;
                    try
                    {
                        using (StreamReader sr = new StreamReader($@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "snapshots")}\snapshots_id{job.Id}.json"))
                        {
                            string json = sr.ReadToEnd();
                            Snapshots = JsonConvert.DeserializeObject<List<Snapshot>>(json) ?? new List<Snapshot>();
                        }
                    }
                    catch
                    {
                        Snapshots = new List<Snapshot>();
                    }
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Running job (ID: {job.Id})...");
                    try
                    {
                        switch (job.Method)
                        {
                            case BackupMethod.Full:
                                jobFull.Backup(job, Snapshots);
                                break;
                            case BackupMethod.Differential:
                                jobDiff.Backup(job, Snapshots);
                                break;
                            case BackupMethod.Incremental:
                                jobIncr.Backup(job, Snapshots);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        job.NextOccurence = (DateTime)cronHandler.GetNextOccurrence(job.Timing, DateTime.UtcNow);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Backup job (ID: {job.Id}) failed: {ex.Message}");
                        job.NextOccurence = (DateTime)cronHandler.GetNextOccurrence(job.Timing, DateTime.UtcNow);
                        try
                        {
                            Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), true);
                        }
                        catch { }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(60 - DateTime.Now.Second));
            }
        }

        public void ForceBackup(List<BackupJob> backupJobs)
        {
            List<Snapshot> Snapshots;

            foreach (var job in backupJobs)
            {
                if (job == null) continue;
                try
                {
                    using (StreamReader sr = new StreamReader($@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "snapshots")}\snapshots_id{job.Id}.json"))
                    {
                        string json = sr.ReadToEnd();
                        Snapshots = JsonConvert.DeserializeObject<List<Snapshot>>(json) ?? new List<Snapshot>();
                    }
                }
                catch
                {
                    Snapshots = new List<Snapshot>();
                }
                Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Forcing job (ID: {job.Id})...");
                try
                {
                    switch (job.Method)
                    {
                        case BackupMethod.Full:
                            jobFull.Backup(job, Snapshots);
                            break;
                        case BackupMethod.Differential:
                            jobDiff.Backup(job, Snapshots);
                            break;
                        case BackupMethod.Incremental:
                            jobIncr.Backup(job, Snapshots);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    job.NextOccurence = (DateTime)cronHandler.GetNextOccurrence(job.Timing, DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Backup job (ID: {job.Id}) failed: {ex.Message}");
                    job.NextOccurence = (DateTime)cronHandler.GetNextOccurrence(job.Timing, DateTime.UtcNow);
                    try
                    {
                        Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Swientek_Backuper", "temp"), true);
                    }
                    catch { }
                }
            }
        }
    }
}
