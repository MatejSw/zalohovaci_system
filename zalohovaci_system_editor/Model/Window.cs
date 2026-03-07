using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalohovaci_system_editor.Model
{
    public interface Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new();

        public void Draw();

        public  void HandleKey(ConsoleKeyInfo keyInfo);
    }
}
