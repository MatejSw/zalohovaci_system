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
            { ConsoleKey.Enter, () => { OnConfigSelected?.Invoke(selectedLine); } }
        };

        public delegate void ConfigSelectedHandler(int index);
        public event ConfigSelectedHandler OnConfigSelected;

        public string Label { get; set; } = "";

        public bool IsActive { get; set; } = false;
        public List<string>? Values { get; set; }
        private int selectedLine = 0;

        public DropdownList(List<string>? values)
        {
            Values = values;
        }

        public void Draw()
        {
            Console.Write(Label);
            Console.SetCursorPosition(Console.CursorLeft - Label.Length + 1, Console.CursorTop + 1);
            for (int i = 0; i < Values.Count; i++)
            {
                if (i == selectedLine && IsActive)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                string value = Values[i].Length < 50 ? $"{Values[i]}" : $"{Values[i].Substring(0, 47)}...";
                Console.Write(value);
                Console.SetCursorPosition(Console.CursorLeft - value.Length, Console.CursorTop + 1);
                Console.ResetColor();
            }
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
