using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;
using zalohovaci_system_editor.Windows;

namespace zalohovaci_system_editor.Components
{
    public class OptionsBox : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        {
            {
                ConsoleKey.RightArrow, () =>
                {
                    selectedValue = (selectedValue + 1) % Options.Count;
                }
            },
            {
                ConsoleKey.LeftArrow, () =>
                {
                    selectedValue--;
                    if (selectedValue < 0) selectedValue = selectedValue + Options.Count;
                }
            },
            {
                ConsoleKey.Enter, () =>
                {
                    IsActive = false;
                }
            }
        };
        public bool Selected { get; set; }
        public bool IsActive { get; set; }

        public string Label;
        public string Value;
        public List<string> Options = new();
        private int? selectedValue = null;

        public OptionsBox()
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
                Console.Write(Value);
            }
            else
            {
                Console.Write($"< {Value} >");
            }
            Console.ResetColor();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (selectedValue == null)
            {
                selectedValue = Options.FindIndex(x => x == Value);
            }
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
            Value = Options[(int)selectedValue];
        }
    }
}
