using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system.Model;
using zalohovaci_system_editor.Components;

namespace zalohovaci_system_editor.Windows
{
    public class EmptyWindow : Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new Dictionary<ConsoleKey, Action>();

        public void Draw()
        {
            
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
