using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;
using zalohovaci_system_editor.Windows;

namespace zalohovaci_system_editor.Components
{
    internal class DoublePane : Module
    {
        public Window LeftWindow { get; set; } = new EmptyWindow();
        public Window RightWindow { get; set; } = new EmptyWindow();

        public bool ActiveSide { get; set; } = false; // false = left, true = right

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"┌{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┐┌{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┐");
            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                Console.WriteLine($"│{"".PadLeft(Console.WindowWidth / 2 - 2, ' ')}││{"".PadLeft(Console.WindowWidth / 2 - 2, ' ')}│");
            }
            Console.Write($"└{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┘└{"".PadLeft(Console.WindowWidth / 2 - 2, '─')}┘");

            Console.SetCursorPosition(2, 2);
            LeftWindow.Draw();

            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 2);
            RightWindow.Draw();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (ActiveSide)
            {
                RightWindow.HandleKey(keyInfo);
            }
            else
            {
                LeftWindow.HandleKey(keyInfo);
            }
        }
    }
}
