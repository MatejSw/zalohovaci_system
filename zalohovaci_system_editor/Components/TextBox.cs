using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    internal class TextBox : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs { get; set; }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
