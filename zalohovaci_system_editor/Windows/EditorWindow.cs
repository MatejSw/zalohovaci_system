using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class EditorWindow : Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>()
        {
            {
                ConsoleKey.Tab, () => { SelectedComponent = ++SelectedComponent % Components.Count; }
            },
            {
                ConsoleKey.Enter, () => 
                { 
                    ComponentSelected?.Invoke();
                }
            }
        };

        public static event Action ComponentSelected;

        public event Action Cancel;
        public event Action SaveChanges;

        public BackupJob SelectedBackupJob { get; set; }

        protected List<IComponent> Components;
        private int SelectedComponent = 0;

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
            Components.Add(new OptionsBox() { Label = "Metoda", Options = ["Full", "Differencial", "Incremental"] });
            Components.Add(new TextBox() { Label = "Časování" });
            Components.Add(new TextBox() { Label = "Počet" });
            Components.Add(new TextBox() { Label = "Velikost" });
            Components.Add(new Button() { Label = "OK", execute = () => { OkButton(); } });
            Components.Add(new Button() { Label = "Storno", execute = () => { Cancel?.Invoke(); } });
        }

        public virtual void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Selected = SelectedComponent == i;
            } 

            Console.Write($"Konfigurace {SelectedBackupJob.Id}");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 2);

            for (int i = 0; i < Components.Count - 4; i++)
            {
                Components[i].Draw();
                Console.SetCursorPosition(cursorLeft, Console.CursorTop + 2);
            }

            Console.Write("Retence:");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Components[5].Draw();
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Components[6].Draw();

            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 2);
            Components[7].Draw();
            Console.SetCursorPosition(Console.CursorLeft + 5, Console.CursorTop);
            Components[8].Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (Components.All(x => !x.IsActive))
            {
                if (KeyInputs.ContainsKey(keyInfo.Key))
                {
                    KeyInputs[keyInfo.Key].Invoke();
                }
            }
            else
            {
                Components[SelectedComponent].HandleKey(keyInfo);
            }

            if (Components[SelectedComponent] is Button button)
            {
                button.HandleKey(keyInfo);
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
            Components[3] = new OptionsBox() { Label = "Metoda", Options = ["Full", "Differencial", "Incremental"], Value = backupJob.Method.ToString() };
            Components[4] = new TextBox() { Label = "Časování", Value = backupJob.Timing };
            Components[5] = new TextBox() { Label = "Počet", Value = backupJob.Retention.Count.ToString() };
            Components[6] = new TextBox() { Label = "Velikost", Value = backupJob.Retention.Size.ToString() };
        }   

        private void OkButton()
        {
            TextBox id = Components[0] as TextBox;
            DropdownList sources = Components[1] as DropdownList;
            DropdownList targets = Components[2] as DropdownList;
            OptionsBox method = Components[3] as OptionsBox;
            TextBox timing = Components[4] as TextBox;
            TextBox retCount = Components[5] as TextBox;
            TextBox retSize = Components[6] as TextBox;

            SelectedBackupJob.Id = id.Value;
            SelectedBackupJob.Sources = sources.Values;
            SelectedBackupJob.Targets = targets.Values;
            SelectedBackupJob.Method = method.Value == "Full" ? BackupMethod.Full : method.Value == "Differencial" ? BackupMethod.Differential : BackupMethod.Incremental;
            SelectedBackupJob.Timing = timing.Value;
            SelectedBackupJob.Retention.Count = Convert.ToInt32(retCount.Value);
            SelectedBackupJob.Retention.Size = Convert.ToInt32(retSize.Value);

            SaveChanges?.Invoke();
            Cancel?.Invoke();
        }
    }
}
