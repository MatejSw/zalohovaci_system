using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalohovaci_system_editor.Model
{
    public interface IComponent
    {
        public Dictionary<ConsoleKey, Action> KeyInputs { get; set; }
        public void Draw();
        public void HandleKey(ConsoleKeyInfo keyInfo);
    }
}
