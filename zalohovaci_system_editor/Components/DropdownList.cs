using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public class DropdownList : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        { 
            { ConsoleKey.DownArrow, () => { selectedLine = Math.Min(++selectedLine, Values.Count - 1); } },
            { ConsoleKey.UpArrow, () => { selectedLine = Math.Max(--selectedLine, 0);; } },
            { ConsoleKey.Enter, () => { OnValueSelected?.Invoke(selectedLine); } }
        };

        public delegate void ConfigSelectedHandler(int index);
        public event ConfigSelectedHandler OnValueSelected;

        public string Label { get; set; } = "";

        public int Height = 20;

        public bool IsActive { get; set; } = false;
        public bool Selected { get; set; }

        public List<string>? Values { get; set; }
        public int selectedLine = 0;

        public DropdownList(List<string>? values)
        {
            Values = values;
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            if (Selected)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write(Label);
            Console.ResetColor();
            Console.SetCursorPosition(cursorLeft + 1, Console.CursorTop + 1);
            int startingIndex = Math.Max(selectedLine - (Height - 1), 0);
            for (int i = Math.Max(selectedLine - (Height - 1), 0); i < Math.Min(Values.Count, startingIndex + Height); i++)
            {
                if (!IsActive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Values[i].Length < 50 ? $"{Values[i]}" : $"{Values[i].Substring(0, 47)}...");
                }
                else if (i == selectedLine && IsActive)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(Values[i].Length < 50 ? $"{Values[i]}" : $"...{Values[i].Substring(Math.Max(Values[i].Length - 47, 0))}");
                }
                else
                {
                    Console.Write(Values[i].Length < 50 ? $"{Values[i]}" : $"{Values[i].Substring(0, 47)}...");
                }
                Console.SetCursorPosition(cursorLeft + 1, Console.CursorTop + 1);
                Console.ResetColor();
            }
            if (Values.Count > Height && selectedLine != Values.Count - 1)
            {
                if (!IsActive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"...");
                Console.ResetColor();
                Console.SetCursorPosition(cursorLeft + 1, Console.CursorTop + 1);
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
        }
    }
}
