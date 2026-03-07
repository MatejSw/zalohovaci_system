using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Services
{
    public class UI
    {
        public Stack<Module> modules = new Stack<Module>();

        public void Draw()
        {
            modules.Peek().Draw();
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            modules.Peek().HandleKey(key);
        }
    }
}
