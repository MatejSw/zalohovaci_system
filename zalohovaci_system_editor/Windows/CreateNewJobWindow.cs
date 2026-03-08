using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Windows
{
    public class CreateNewJobWindow : EditorWindow
    {
        public CreateNewJobWindow() : base()
        {
            Components[Components.Count - 2] = new Button() { Label = "Vytvořit", execute = () => { CreateNewJob(); } };
            Components[Components.Count - 1] = new Button() { Label = "Storno", execute = () => { Cancel?.Invoke(); } };
        }

        public override void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            Console.Write("Vytvoření nové zálohovací konfigurace");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 2);
            base.Draw();
        }
    
        private void CreateNewJob()
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

            Create?.Invoke(SelectedBackupJob.Id);
        }
    
        public event Action<string> Create;
        public event Action Cancel;
    }
}
