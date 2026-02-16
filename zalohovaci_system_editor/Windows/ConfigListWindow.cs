using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Windows
{
    public class ConfigListWindow : Window
    {
        public override Dictionary<ConsoleKey, Action> KeyInputs { get; set; }

        public override void Draw()
        {
            Console.Write("Test Levé okno");
            Console.SetCursorPosition(2, Console.CursorTop + 2);
            Console.Write("Test Levé okno řádek 1");
            Console.SetCursorPosition(2, Console.CursorTop + 1);
            Console.Write("Test Levé okno řádek 2");
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
        }
    }
}
