using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;
using zalohovaci_system_editor.Windows;

namespace zalohovaci_system_editor.Components
{
    public class Button : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>()
        {
            {
                ConsoleKey.Enter, () => { execute.Invoke(); }
            }
        };

        public Button()
        {
            EditorWindow.ComponentSelected += () =>
            {
                if (Selected) execute();
            };
        }

        public bool Selected { get; set; }

        public Action execute;

        public string Label;

        public void Draw()
        {
            if (Selected)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write($"[ {Label} ]");
            Console.ResetColor();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
