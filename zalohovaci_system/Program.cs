using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using zalohovaci_system.Model;
using zalohovaci_system.Services;
using zalohovaci_system_api;

namespace zalohovaci_system
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyContext context = new MyContext();

            List<zalohovaci_system.Model.BackupJob> backupJobs = new();

            IPHostEntry? host = Dns.GetHostEntry(Dns.GetHostName());

            string ip = host.AddressList
                     .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork &&
                                          !IPAddress.IsLoopback(a)).ToString();

            if (!context.Client.Any(x => x.ip == ip))
            {
                context.Client.Add(new()
                {
                    ip = ip,
                    pcname = Environment.MachineName
                });
                context.SaveChanges();
            }

            int id = context.Client.Where(x => x.ip == ip).FirstOrDefault().id;

            for (int i = 0; i < context.BackupJobs.Count(); i++)
            {
                zalohovaci_system_api.Models.BackupJob backupJob = context.BackupJobs.ToList()[i];

                if (!context.ClientsToJobs.Where(x => x.clientId == id).Any(x => x.jobId == backupJob.Id)) continue;

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
            BackupService backupService = new BackupService();

            Console.CursorVisible = false;
            Console.WriteLine("Starting backup service...");
            Console.WriteLine("Press ESC to stop the service.");
            Console.WriteLine("Press F to force backup.");
            Console.WriteLine();
            Console.WriteLine("--------------- LOG ---------------"); 
            Console.WriteLine();

            backupService = new BackupService();
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Backup service is running...");
            backupService.RunBackupJobs(backupJobs);

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape) break;
                if (key == ConsoleKey.F)
                {
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Forcing Backup...");
                    backupService.ForceBackup(backupJobs);
                }
            }
        }
    }
}
