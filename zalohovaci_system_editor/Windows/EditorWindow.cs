using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Windows
{
    public class EditorWindow : Window
    {
        public override Dictionary<ConsoleKey, Action> KeyInputs { get; set; }

        public override void Draw()
        {
            Console.Write("Test Pravé okno");
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
