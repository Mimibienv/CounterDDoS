using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VortexLibraries;
using System.Diagnostics;

namespace CounterDDoS
{
    sealed internal class Program
    {
        static string[][] options =
        {
            new[] {"Network", "Windows", "Discord"},
            new[] { "Roblox", "Mail Bomb" }
        };

        static int selectedRow = 0;
        static int selectedColumn = 0;
        static int OptionWidth = 12;

        static async Task Main()
        {
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                DisplayMenu();

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow         && selectedRow > 0 && selectedColumn < options[selectedRow-1].Length && options[selectedRow-1][selectedColumn] != null) { selectedRow--; }
                else if (key == ConsoleKey.DownArrow  && selectedRow < options.Length-1 && selectedColumn < options[selectedRow+1].Length && options[selectedRow+1][selectedColumn] != null) { selectedRow++; }
                else if (key == ConsoleKey.LeftArrow  && selectedColumn > 0 && options[selectedRow][selectedColumn-1] != null) { selectedColumn--; }
                else if (key == ConsoleKey.RightArrow && selectedColumn < options[selectedRow].Length-1 && options[selectedRow][selectedColumn+1] != null) { selectedColumn++; }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    //Console.WriteLine($"You selected: {options[selectedRow][selectedColumn]}");
                    switch (options[selectedRow][selectedColumn])
                    {
                        case "Network":
                            await VortexHttp.Init();
                            break;

                        default:
                            Console.WriteLine("kys we didn't script that yet");
                            await Task.Delay(-1);
                            break;
                    }
                    break;
                }
            }
        }

    static void DisplayMenu()
        {
            TitlePrint.Print();
            Console.WriteLine("\n");

            int TotalWidth = (OptionWidth + 4) * 3 + 3 + 2;
            Action center = () => { Console.SetCursorPosition((Console.WindowWidth - TotalWidth) / 2, Console.CursorTop); };
            Action SetFrameColor = () => { Console.ForegroundColor = ConsoleColor.DarkMagenta; };

            SetFrameColor();
            center(); Console.WriteLine("╔" + new string('═', TotalWidth - 2) + "╗");
            center(); Console.WriteLine("║" + new string(' ', TotalWidth - 2) + "║");
            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - TotalWidth)/2, Console.CursorTop);
                SetFrameColor();
                Console.Write("║   ");
                for (int j = 0; j < options[i].Length; j++)
                {
                    if (options[i][j] == null)
                    {
                        Console.Write(new string(' ', OptionWidth+4));
                        continue;
                    }

                    if (i == selectedRow && j == selectedColumn)
                    {
                        /*Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"> {options[i][j]} ");
                        Console.ResetColor();*/
                        string SpaceLeft = new string(' ', OptionWidth - options[i][j].Length);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"> {"["+options[i][j]+"]"}{SpaceLeft}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        string SpaceLeft = new string(' ', OptionWidth - options[i][j].Length);
                        Console.Write($"  [{options[i][j]}]{SpaceLeft}");
                    }
                }
                Console.SetCursorPosition((Console.WindowWidth + TotalWidth)/2-1, Console.CursorTop);
                SetFrameColor();
                Console.Write("║");
                Console.WriteLine();
            }

            SetFrameColor();
            center(); Console.WriteLine("║" + new string(' ', TotalWidth - 2) + "║");
            center(); Console.WriteLine("╚" + new string('═', TotalWidth - 2) + "╝");
        }
    }
}