using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public class DialogueBox : Module
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new();
        public Window Window { get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.WindowHeight / 2 - 6);
            Console.Write("┌" + "".PadRight(48, '─') + "┐");
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.CursorTop + 1);
                Console.Write("│" + "".PadRight(48) + "│");
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 25, Console.CursorTop + 1);
            Console.Write("└" + "".PadRight(48, '─') + "┘");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 23, Console.WindowHeight / 2 - 5);
            Window.Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            Window.HandleKey(keyInfo);
        }
    }
}
