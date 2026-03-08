using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class ChangeDriveWindow : Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        {
            {
                ConsoleKey.Tab, () =>
                {
                    components[SelectedComponent].Selected = false;
                    SelectedComponent = (SelectedComponent + 1) % components.Count;
                    components[SelectedComponent].Selected = true;
                }
            }
        };

        List<IComponent> components = new List<IComponent>() { };

        private int SelectedComponent = 0;

        public event Action Cancel;
        public event Action<string> Confirm;

        public ChangeDriveWindow()
        {
            components.Add(new TextBox());
            components.Add(new Button() { Label = "OK", execute = () => { CreateNewJob(); } });
            components.Add(new Button() { Label = "Storno", execute = () => { Cancel?.Invoke(); } });
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;

            if (SelectedComponent == 0)
            {
                components[0].IsActive = true;
            }
            else
            {
                components[0].IsActive = false;
            }

            Console.Write("Napište písmeno disku:");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 2);
            components[0].Draw();
            Console.SetCursorPosition(cursorLeft + 2, Console.CursorTop + 4);
            components[1].Draw();
            Console.SetCursorPosition(Console.CursorLeft + 25, Console.CursorTop);
            components[2].Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
            else
            {
                components[SelectedComponent].HandleKey(keyInfo);
            }
        }

        private void CreateNewJob()
        {
            TextBox textBox = (TextBox)components[0];
            Confirm?.Invoke(textBox.Value);
        }
    }
}
