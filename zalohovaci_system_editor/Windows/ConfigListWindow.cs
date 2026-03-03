using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class ConfigListWindow : Window
    {
        static private List<BackupJob> BackupJobs;
        public delegate void ConfigSelectedHandler(BackupJob backupJob);
        public event ConfigSelectedHandler OnConfigSelected;

        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>();

        public bool Selected { get; set; }
        public bool IsActive { get; set; }

        private static List<string> ConfigNames = new();
        private DropdownList dropdownList;

        public ConfigListWindow(List<BackupJob> backupJobs)
        {
            BackupJobs = backupJobs;
            for (int i = 0; i < backupJobs.Count; i++)
            {
                ConfigNames.Add($"Konfigurace {backupJobs[i].Id}");
            }
            dropdownList = new DropdownList(ConfigNames);
            dropdownList.IsActive = true;
            dropdownList.OnConfigSelected += (index) =>
            {
                OnConfigSelected?.Invoke(BackupJobs[index]);
            };
        }

        public void Draw()
        {
            Console.SetCursorPosition(2, 2);
            Console.Write("Seznam zálohovacích konfigurací:");

            Console.SetCursorPosition(2, 4);

            dropdownList.Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
            else
            {
                dropdownList.HandleKey(keyInfo);
            }
        }

        public void SaveBackup(BackupJob backupJob)
        {
            BackupJobs[dropdownList.selectedLine] = backupJob;
            dropdownList.Values[dropdownList.selectedLine] = $"Konfigurace {backupJob.Id}";
        }
    }
}
