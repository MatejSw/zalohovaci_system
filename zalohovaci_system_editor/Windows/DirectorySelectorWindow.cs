using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalohovaci_system_editor.Components;
using zalohovaci_system_editor.Model;

namespace zalohovaci_system_editor.Windows
{
    public class DirectorySelectorWindow : Window
    {
        public Stack<string> directories;

        private DropdownList dropdownList;

        public event Action<string> OnDirectorySelected;
        public event Action SwitchWindow;
        public event Action ChangeDriveEvent;

        public Dictionary<ConsoleKey, Action> KeyInputs => new()
        {
            {
                ConsoleKey.Spacebar, () =>
                {
                    string value = directories.Peek() + "\\" + dropdownList.Values[dropdownList.selectedLine];
                    OnDirectorySelected?.Invoke(value.Substring(0, 2) + value.Substring(3));
                }
            },
            {
                ConsoleKey.Tab, () =>
                {
                    dropdownList.IsActive = false;
                    SwitchWindow?.Invoke();
                }
            },
            {
                ConsoleKey.Insert, () =>
                {
                    ChangeDriveEvent?.Invoke();
                }
            }
        };

        public DirectorySelectorWindow()
        {
            directories = new Stack<string>();
            directories.Push(@"C:\");

            dropdownList = new DropdownList(new List<string>());
            dropdownList.Height = 17;
            dropdownList.OnValueSelected += (index) =>
            {
                string selectedDirectory = dropdownList.Values[index];
                if (selectedDirectory == "..")
                {
                    if (directories.Count > 1)
                    {
                        directories.Pop();
                    }
                }
                else
                {
                    string value = directories.Peek() + "\\" + selectedDirectory;
                    // Add only if it's a directory, not a file
                    if (Directory.Exists(value))
                    {
                        directories.Push(value);
                        dropdownList.selectedLine = 0;
                    }
                }
            };
        }

        public void Draw()
        {
            int cursorLeft = Console.CursorLeft;
            dropdownList.Values.Clear();
            dropdownList.Values.Add("..");
            try
            {
                List<string> list = new List<string>();
                List<string> fullDirectory = new List<string>();
                fullDirectory.AddRange(Directory.GetDirectories(directories.Peek()));
                fullDirectory.AddRange(Directory.GetFiles(directories.Peek()));
                foreach (string directory in fullDirectory)
                {
                    list.Add(directory.Split('\\').Last());
                }
                dropdownList.Values.AddRange(list);

            }
            catch
            {
               
            }

            Console.Write("Vyberte cílové adresáře pro zálohování:");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write($"Aktuální složka: {(directories.Peek().Length < 30 ? directories.Peek() : "..." + directories.Peek().Substring(directories.Peek().Length - 27))}");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);

            dropdownList.Draw();

            Console.SetCursorPosition(cursorLeft, Console.WindowHeight - 6);
            Console.Write("SPACE - Vybrat soubor");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("ENTER - Vstoupit do složky");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("INSERT - Vyměnit disk");
            Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
            Console.Write("TAB - Přepnout panel");
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            if (KeyInputs.ContainsKey(keyInfo.Key))
            {
                KeyInputs[keyInfo.Key].Invoke();
            }
            else
            {
                dropdownList.HandleKey(keyInfo);
            }
        }

        public void SelectWindow()
        {
            dropdownList.IsActive = true;
        }

        public void ChangeDrive(string drive)
        {
            directories.Clear();
            string value = drive.Substring(0,1) + ":\\";
            directories.Push(value);
            dropdownList.selectedLine = 0;
        }
    }
}
