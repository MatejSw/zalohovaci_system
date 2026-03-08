using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public class SinglePane : Module
    {
        public Window ParentWindow { get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("┌" + "".PadRight(Console.WindowWidth - 2, '─') + "┐");
            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.WriteLine("│" + "".PadRight(Console.WindowWidth - 2) + "│");
            }
            Console.Write("└" + "".PadRight(Console.WindowWidth - 2, '─') + "┘");

            Console.SetCursorPosition(2, 2);
            ParentWindow.Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            ParentWindow.HandleKey(keyInfo);
        }
    }
}
