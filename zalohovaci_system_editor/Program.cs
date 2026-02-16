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
            ConfigListWindow configListWindow = new ConfigListWindow();
            EditorWindow editorWindow = new EditorWindow();
            UI uI = new UI()
            {
                LeftWindow = configListWindow,
                RightWindow = editorWindow
            };

            List<BackupJob> backupJobs = JsonConvert.DeserializeObject<List<BackupJob>>(File.ReadAllText(@"X:\P3.B\Programování\Projekty\zalohovaci_system\zalohovaci_system\conf\backup_config.json")) ?? new List<BackupJob>();

            string jobList = "";
            for (int i = 0; i < backupJobs.Count; i++)
            {
                jobList += $"Konfigurace {backupJobs[i].Id}\n";
            }

            while (true)
            {
                uI.Draw();
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (uI.consoleKeys.Contains(key.Key))
                {
                    uI.HandleKey(key.Key);
                }
            }
        }
    }
}
