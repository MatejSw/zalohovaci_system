using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Services
{
    public class UI
    {
        public Window LeftWindow { get; set; }
        public Window RightWindow { get; set; }

        public bool ActiveSide { get; set; } = false; // false = left, true = right

        public UI()
        {
            
        }

        public void Draw()
        {
            Console.Clear();
            Console.WriteLine($"┌{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┐┌{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┐");
            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.WriteLine($"│{"".PadLeft(Console.WindowWidth / 2 - 2, ' ')}││{"".PadLeft(Console.WindowWidth / 2 - 2, ' ')}│");
            }
            Console.Write($"└{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┘└{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┘");

            LeftWindow.Draw();

            RightWindow.Draw();
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            if (ActiveSide)
            {
                RightWindow.HandleKey(key);
            }
            else
            {
                LeftWindow.HandleKey(key);
            }
        }
    }
}
