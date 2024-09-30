using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Gamecollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int marker = 0;
            int newMarker = 0;
            int[] spil = new int[4] { 1, 2, 3, 4 };
            string minesweeper = Directory.GetCurrentDirectory() + @"\Minesweeper_simon.exe";
            string jeopardy = Directory.GetCurrentDirectory() + @"\Jeopardy_Irene.exe";
            string battleships = Directory.GetCurrentDirectory() + @"\Rikke.exe";
            string mastermind = Directory.GetCurrentDirectory() + @"\Philip_Mastermind.exe";
            bool exit;
            bool input;
            do
            {
                Console.Clear();
                input = false;
                exit = false;
                newMarker += marker;
                if (newMarker < 0)
                {
                    newMarker = 0;
                }
                if (newMarker > 3)
                {
                    newMarker = 3;
                }
                Console.WriteLine("Velkommen til vores spilsamling");
                Console.WriteLine("Vælg et spil nedenfor(pil op og ned + enter), eller tryk escape for at lukke");
                for (int i = 0; i < spil.Length; i++)
                {
                    if (i == newMarker)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine((Games)spil[i]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                PlayerInput(out marker, out exit, out input);
                if (input)
                {
                    switch (newMarker)
                    {
                        case 0:
                            Process.Start(minesweeper);
                            exit = true;
                            break;
                        case 1:
                            Process.Start(mastermind);
                            exit = true;
                            break;
                        case 2:
                            Process.Start(battleships);
                            exit = true;
                            break;
                        case 3:
                            Process.Start(jeopardy);
                            exit = true;
                            break;
                    }
                }
            }
            while (!exit);
            if (exit && !input)
            {
                Console.WriteLine("Tak fordi du gad at spille med os!");
                Console.WriteLine("Hilsen Irene, Rikke, Philip og Simon");
                Console.WriteLine("Tryk på en tast for at afslutte");
                Console.ReadKey();
            }
        }
        enum Games : int
        {
            Minestryger = 1,
            Mastermind = 2,
            Sænkeslagskib = 3,
            Jeopardy = 4
        }
        static void PlayerInput(out int x, out bool exit, out bool input)
        {
            x = 0;
            exit = false;
            input = false;
            var keyInput = Console.ReadKey(intercept: true);
            switch (keyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    x--;
                    break;
                case ConsoleKey.DownArrow:
                    x++;
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                case ConsoleKey.Enter:
                    input = true;
                    break;
                default:
                    break;
            }
        }
    }
}
