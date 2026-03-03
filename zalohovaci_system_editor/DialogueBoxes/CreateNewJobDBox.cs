using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.DialogueBoxes
{
    internal class CreateNewJobDBox : DialogueBox
    {
        public bool Selected { get; set; }
        public bool IsActive { get; set; }

        public void Draw()
        {
            Console.Write($"┌{"".PadLeft(25, '─')}┐");
            Console.SetCursorPosition(Console.CursorLeft - 27, Console.CursorTop);
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"│{"".PadLeft(25)}│");
                Console.SetCursorPosition(Console.CursorLeft - 27, Console.CursorTop);
            }
            Console.Write($"└{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┘");
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
