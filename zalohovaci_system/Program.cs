using System.Text.Json;
using Newtonsoft.Json;
using zalohovaci_system.Model;
using zalohovaci_system.Services;

namespace zalohovaci_system
{
    public class Program
    {
        static void Main(string[] args)
        { 
            List<BackupJob> backupJobs = JsonConvert.DeserializeObject<List<BackupJob>>(File.ReadAllText(@"X:\P3.B\Programování\Projekty\zalohovaci_system\zalohovaci_system\conf\backup_config.json")) ?? new List<BackupJob>();
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
                    Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}:{DateTime.Now.Second.ToString().PadLeft(2, '0')}] Forcing backup...");
                    backupService.ForceBackup(backupJobs);
                }
            }
        }
    }
}
