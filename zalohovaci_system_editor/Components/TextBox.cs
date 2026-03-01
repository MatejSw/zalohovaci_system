using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    internal class TextBox : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new();

        public string Label { get; set; }
        public string Value { get; set; }

        public void Draw()
        {
            Console.Write(Label);
            Console.SetCursorPosition(Console.CursorLeft - Label.Length + 1, Console.CursorTop + 1);
            Console.Write(Value);
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
