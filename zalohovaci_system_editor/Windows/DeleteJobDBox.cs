using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Windows
{
    public class DeleteJobDBox : Window
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

        private List<IComponent> components = new List<IComponent>() { };
        private int SelectedComponent = 0;

        public event Action<string> Delete;
        public event Action Cancel;

        public string Id { get; set; }

        public DeleteJobDBox()
        {
            components.Add(new Button() { Label = "Ano", execute = () => { Delete?.Invoke(Id); }, Selected = true });
            components.Add(new Button() { Label = "Ne", execute = () => { Cancel?.Invoke(); } });
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            Console.Write("Opravdu chcete smazat zálohovací konfiguraci?");
            Console.SetCursorPosition(cursorLeft + 1, Console.CursorTop + 2);
            Console.Write($"Konfigurace {Id}");
            Console.SetCursorPosition(cursorLeft + 2, Console.CursorTop + 5);
            components[0].Draw();
            Console.SetCursorPosition(cursorLeft + 25, Console.CursorTop);
            components[1].Draw();
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
    }
}
