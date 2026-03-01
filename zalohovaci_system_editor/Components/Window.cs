using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public interface Window : IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new();

        public void Draw();

        public  void HandleKey(ConsoleKeyInfo keyInfo);
    }
}
