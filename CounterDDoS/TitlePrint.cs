using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

using System.Drawing;
using Pastel;

namespace VortexLibraries
{
    internal static class TitlePrint
    {
        internal static void Print() => PrintVortex();
        private static void PrintVortex() {
            string s = $@"
 ██▒   █▓  ▒█████    ██▀███   ▄▄▄█████▓ ▓█████  ▒██   ██▒
▓██░   █▒ ▒██▒  ██▒ ▓██ ▒ ██▒ ▓  ██▒ ▓▒ ▓█   ▀  ▒▒ █ █ ▒░
 ▓██  █▒░ ▒██░  ██▒ ▓██ ░▄█ ▒ ▒ ▓██░ ▒░ ▒███    ░░  █   ░
  ▒██ █░░ ▒██   ██░ ▒██▀▀█▄   ░ ▓██▓ ░  ▒▓█  ▄   ░ █ █ ▒ 
   ▒▀█░   ░ ████▓▒░ ░██▓ ▒██▒   ▒██▒ ░  ░▒████▒ ▒██▒ ▒██▒
   ░ ▐░   ░ ▒░▒░▒░  ░ ▒▓ ░▒▓░   ▒ ░░    ░░ ▒░ ░ ▒▒ ░ ░▓ ░
   ░ ░░     ░ ▒ ▒░    ░▒ ░ ▒░     ░      ░ ░  ░ ░░   ░▒ ░
     ░░   ░ ░ ░ ▒     ░░   ░    ░    v1.1  ░     ░    ░  
      ░       ░ ░      ░                   ░  ░  ░    ░  
               ░                                   By emirarmin and Mimibienv {"\n"}";

            int c = 0;
            int _c = 0;
            bool swapcolor = false;

            Console.SetCursorPosition(Console.CursorLeft, 2);
            foreach (var sequence in s.Split("\n"))
            {
                Console.SetCursorPosition(Console.CursorLeft + (Console.WindowWidth - sequence.Length) / 2, Console.CursorTop);
                foreach (var chr in sequence.Select(c => c.ToString())){
                    if (!swapcolor && chr == "B")
                        swapcolor = true;
                    Console.Write(chr.Pastel(
                        swapcolor? Color.FromArgb(c, 0, _c)
                        : Color.FromArgb(200 - c, Abs(100 - _c), 200 - _c)
                        ));
                    _c+=1;
                }
                Console.WriteLine();
                c += 15;
                _c = 5 * (c / 15);
            }

        }
    }
}
