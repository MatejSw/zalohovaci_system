using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;
using zalohovaci_system_editor.Windows;

namespace zalohovaci_system_editor.Components
{
    internal class TextBox : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        {
            {
                ConsoleKey.Enter, () =>
                {
                    IsActive = false;
                }
            }
        };

        public string Label { get; set; } = "";
        public string Value { get; set; } = "";

        public bool Selected { get; set; }
        public bool IsActive { get; set; }

        public TextBox()
        {
            EditorWindow.ComponentSelected += () =>
            {
                if (Selected) IsActive = true;
            };
        }

        public void Draw()
        {
            if (Selected)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            if (IsActive)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write(Label);
            Console.ResetColor();
            Console.SetCursorPosition(Console.CursorLeft - Label.Length + 1, Console.CursorTop + 1);
            if (!IsActive)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.Write(Value);
            if (IsActive)
            {
                Console.Write("_");
            }
            Console.ResetColor();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (IsActive)
            {
                if (KeyInputs.ContainsKey(keyInfo.Key)) KeyInputs[keyInfo.Key]?.Invoke();
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        Value = Value.Substring(0, Math.Max(Value.Length - 1,0));
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Value += keyInfo.KeyChar;
                    }
                }
            }
        }
    }
}
