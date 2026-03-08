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

        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>()
        {
            {
                ConsoleKey.Insert, () =>
                {
                    CreateNewJob?.Invoke();
                }
            },
            {
                ConsoleKey.Delete, () =>
                {
                    DeleteSelectedJob?.Invoke(BackupJobs[dropdownList.selectedLine]);
                }
            }
        };

        private static List<string> ConfigNames;
        private DropdownList dropdownList;

        public event Action CreateNewJob;
        public event Action<BackupJob> DeleteSelectedJob;

        public ConfigListWindow(List<BackupJob> backupJobs)
        {
            ConfigNames = new List<string>();
            BackupJobs = backupJobs;
            for (int i = 0; i < backupJobs.Count; i++)
            {
                ConfigNames.Add($"Konfigurace {backupJobs[i].Id}");
            }
            dropdownList = new DropdownList(ConfigNames);
            dropdownList.IsActive = true;
            dropdownList.OnValueSelected += (index) =>
            {
                OnConfigSelected?.Invoke(BackupJobs[index]);
            };
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(cursorLeft, 2);
            Console.Write("Seznam zálohovacích konfigurací:");

            Console.SetCursorPosition(cursorLeft, 4);

            dropdownList.Draw();

            Console.SetCursorPosition(cursorLeft, Console.WindowHeight - 4);
            Console.Write("INSERT - Vytvořit novou konfiguraci");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("DELETE - Smazat vybranou konfiguraci");
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

        public void AddBackup(BackupJob backupJob)
        {
            BackupJobs.Add(backupJob);
            dropdownList.Values.Add($"Konfigurace {backupJob.Id}");
        }

        public void RemoveBackup(BackupJob backupJob)
        {
            int index = BackupJobs.IndexOf(backupJob);
            if (index >= 0)
            {
                BackupJobs.RemoveAt(index);
                dropdownList.Values.RemoveAt(index);
            }
            if (dropdownList.selectedLine >= dropdownList.Values.Count)
            {
                dropdownList.selectedLine = dropdownList.Values.Count - 1;
            }
        }
    }
}
