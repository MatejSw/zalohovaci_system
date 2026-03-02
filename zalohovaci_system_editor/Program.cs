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

            List<BackupJob> backupJobs = JsonConvert.DeserializeObject<List<BackupJob>>(File.ReadAllText(@"X:\P3.B\Programování\Projekty\zalohovaci_system\zalohovaci_system\conf\backup_config.json")) ?? new List<BackupJob>();

            ConfigListWindow configListWindow = new ConfigListWindow(backupJobs);
            EditorWindow editorWindow = new EditorWindow();
            EmptyWindow emptyWindow = new EmptyWindow();
            UI uI = new UI();
            uI.PushWindow(emptyWindow, true);
            uI.PushWindow(configListWindow, false);

            configListWindow.OnConfigSelected += (backupJob) =>
            {
                editorWindow.LoadBackupJob(backupJob);
                uI.ActiveSide = true;
                uI.PushWindow(editorWindow, true);
            };

            editorWindow.Cancel += () =>
            {
                uI.ActiveSide = false;
                uI.PopWindow(true);
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
