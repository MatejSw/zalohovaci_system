using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class EditorWindow : Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>();

        public BackupJob SelectedBackupJob { get; set; }

        private List<IComponent> Components;

        public EditorWindow()
        {
            Components = new List<IComponent>();
            Components.Add(new TextBox() { Label = "ID"} );
            Components.Add(new DropdownList(null)
            {
                Label = "Zdroje"
            });
            Components.Add(new DropdownList(null)
            {
                Label = "Cíle"
            });
            Components.Add(new TextBox() { Label = "Metoda" });
            Components.Add(new TextBox() { Label = "Časování" });
            Components.Add(new TextBox() { Label = "Počet" });
            Components.Add(new TextBox() { Label = "Velikost" });
        }

        public void Draw()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 2);
            Console.Write($"Konfigurace {SelectedBackupJob.Id}");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 4);

            for (int i = 0; i < Components.Count - 2; i++)
            {
                Components[i].Draw();
                Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.CursorTop + 2);
            }

            Console.Write("Retence:");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 3, Console.CursorTop + 1);
            Components[5].Draw();
            Console.SetCursorPosition(Console.WindowWidth / 2 + 3, Console.CursorTop + 1);
            Components[6].Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
        }

        public void LoadBackupJob(BackupJob backupJob)
        {
            SelectedBackupJob = backupJob;
            Components[0] = new TextBox() { Label = "ID", Value = backupJob.Id.ToString() };
            Components[1] = new DropdownList(backupJob.Sources)
            {
                Label = "Zdroje",
                Values = backupJob.Sources
            };
            Components[2] = new DropdownList(backupJob.Targets)
            {
                Label = "Cíle",
                Values = backupJob.Targets
            };
            Components[3] = new TextBox() { Label = "Metoda", Value = backupJob.Method.ToString() };
            Components[4] = new TextBox() { Label = "Časování", Value = backupJob.Timing };
            Components[5] = new TextBox() { Label = "Počet", Value = backupJob.Retention.Count.ToString() };
            Components[6] = new TextBox() { Label = "Velikost", Value = backupJob.Retention.Size.ToString() };
        }   
    }
}
