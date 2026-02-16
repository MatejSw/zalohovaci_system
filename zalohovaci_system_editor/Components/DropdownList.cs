using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public class DropdownList : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs { get; set; } = new Dictionary<ConsoleKey, Action>()
        { 
            { ConsoleKey.UpArrow, () => { selectedLine = Math.Min(++selectedLine, Values.Count - 1); } },
            { ConsoleKey.DownArrow, () => { selectedLine = Math.Max(--selectedLine, 0);; } }
        };

        public static List<string> Values { get; set; }
        private static int selectedLine = 0;

        public void Draw()
        {
            throw new NotImplementedException();
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
