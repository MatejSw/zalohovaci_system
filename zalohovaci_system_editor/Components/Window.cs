using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Components
{
    public abstract class Window : IComponent
    {
        public abstract Dictionary<ConsoleKey, Action> KeyInputs { get; set; }

        public abstract void Draw();

        public abstract void HandleKey(ConsoleKeyInfo keyInfo);
    }
}
