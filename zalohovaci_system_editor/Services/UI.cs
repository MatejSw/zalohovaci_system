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
        public Stack<Window> LeftWindow { get; set; } = new Stack<Window>();
        public Stack<Window> RightWindow { get; set; } = new Stack<Window>();

        public bool ActiveSide { get; set; } = false; // false = left, true = right

        public void PushWindow(Window window, bool side)
        {
            if (side)
            {
                RightWindow.Push(window);
            }
            else
            {
                LeftWindow.Push(window);
            }
        }

        public void PopWindow(bool side)
        {
            if (side)
            {
                RightWindow.Pop();
            }
            else
            {
                LeftWindow.Pop();
            }
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

            LeftWindow.Peek().Draw();

            RightWindow.Peek().Draw();
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            if (ActiveSide)
            {
                RightWindow.Peek().HandleKey(key);
            }
            else
            {
                LeftWindow.Peek().HandleKey(key);
            }
        }
    }
}
