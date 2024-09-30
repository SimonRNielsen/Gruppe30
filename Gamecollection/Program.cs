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
            //Booleans and integers to react and record player input
            bool exit;
            bool input;
            int marker = 0;
            int newMarker = 0;
            //String array with name of games
            string[] games = new string[4] {"Minestryger", "Jeopardy", "Sænke Slagskibe", "Mastermind"};
            //Strings with path to game executables, which are added to same basefolder as "Gamecollection.exe" by referencing it in the project
            string minesweeper = Directory.GetCurrentDirectory() + @"\Minesweeper_simon.exe";
            string jeopardy = Directory.GetCurrentDirectory() + @"\Jeopardy_Irene.exe";
            string battleships = Directory.GetCurrentDirectory() + @"\Rikke.exe";
            string mastermind = Directory.GetCurrentDirectory() + @"\Philip_Mastermind.exe";
            //Do-While loop to run a minimum of once
            do
            {
                //Clears screen to avoid clutter
                Console.Clear();
                //Updates player marker-position
                newMarker += marker;
                //Keeps marker inside bounds
                if (newMarker < 0)
                {
                    newMarker = 0;
                }
                if (newMarker > 3)
                {
                    newMarker = 3;
                }
                //Welcome message and info on how to input selection
                Console.WriteLine("Velkommen til vores spilsamling");
                Console.WriteLine("Vælg et spil nedenfor(pil op og ned + enter), eller tryk escape for at lukke");
                //Draws array on a line-by-line basis
                for (int i = 0; i < games.Length; i++)
                {
                    //Highlights player marker
                    if (i == newMarker)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(games[i]);
                }
                //Resets colorscheme to avoid most of console turning green
                Console.BackgroundColor = ConsoleColor.Black;
                PlayerInput(out marker, out exit, out input);
                //Starts 1 of 4 games up depending on player selection
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
            //Loops selection progress
            while (!exit);
            //Displays farewell message if no program was initiated
            if (exit && !input)
            {
                Console.WriteLine("Tak fordi du gad at spille med os!");
                Console.WriteLine("Hilsen Irene, Rikke, Philip og Simon");
                Console.WriteLine("Tryk på en tast for at afslutte");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Detects playerinput
        /// </summary>
        /// <param name="x">Changes movement on x-axis</param>
        /// <param name="exit">Logs "escape" being pressed</param>
        /// <param name="input">Logs "enter" being pressed</param>
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
