using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Create?.Invoke(SelectedBackupJob.Id);
        }
    
        public event Action<string> Create;
        public event Action Cancel;
    }
}
