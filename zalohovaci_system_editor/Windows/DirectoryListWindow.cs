using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class DirectoryListWindow : Window
    {
        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        {
            { ConsoleKey.Delete, () => 
                { 
                    dropdownList.Values.RemoveAt(dropdownList.selectedLine);
                    if (dropdownList.selectedLine >= dropdownList.Values.Count)
                    {
                        dropdownList.selectedLine = Math.Max(dropdownList.Values.Count - 1, 0);
                    }
                } 
            },
            { ConsoleKey.Tab, () =>
                {
                    OkButton.Selected = false;
                    CancelButton.Selected = false;
                    dropdownList.IsActive = false;
                    SwitchWindow?.Invoke();
                }
            },
            {
                ConsoleKey.LeftArrow, () =>
                {
                    if (dropdownList.Values.Count > 0)
                    {
                        selectedButton = !selectedButton;
                        OkButton.Selected = selectedButton;
                        CancelButton.Selected = !selectedButton;
                    }
                }
            },
            {
                ConsoleKey.RightArrow, () =>
                {
                    if (dropdownList.Values.Count > 0)
                    {
                        selectedButton = !selectedButton;
                        OkButton.Selected = selectedButton;
                        CancelButton.Selected = !selectedButton;
                    }
                }
            }
        };

        public event Action SwitchWindow;
        public event Action<List<string>> SaveDirectories;
        public event Action Cancel;

        private DropdownList dropdownList;

        private Button OkButton;
        private Button CancelButton;

        private bool selectedButton;

        public DirectoryListWindow()
        {
            dropdownList = new DropdownList(new List<string>());
            dropdownList.Height = 18;
            OkButton = new Button() { Label = "OK", execute = () => { SaveDirectories?.Invoke(dropdownList.Values); } };
            CancelButton = new Button() { Label = "Storno", execute = () => { Cancel?.Invoke(); } };
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            Console.Write("Vybrané složky pro zálohování: ");

            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            dropdownList.Draw();

            Console.SetCursorPosition(cursorLeft, Console.WindowHeight - 5);
            OkButton.Draw();
            Console.SetCursorPosition(cursorLeft + 25, Console.CursorTop);
            CancelButton.Draw();
            Console.SetCursorPosition(cursorLeft, Console.WindowHeight - 3);
            Console.Write("DELETE - Odstranit adresář");
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (selectedButton)
                {
                    OkButton.HandleKey(keyInfo);
                }
                else
                {
                    CancelButton.HandleKey(keyInfo);
                }
            }
            else
            {
                dropdownList.HandleKey(keyInfo);
            }
        }

        public void SetDirectories(List<string> directories)
        {
            dropdownList.Values.Clear();
            dropdownList.Values.AddRange(directories);
        }

        public void AddDirectory(string directory)
        {
            dropdownList.Values.Add(directory);
        }

        public void SelectWindow()
        {
            dropdownList.IsActive = true;
            selectedButton = true;
            OkButton.Selected = selectedButton;
            CancelButton.Selected = !selectedButton;
        }
    }
}
