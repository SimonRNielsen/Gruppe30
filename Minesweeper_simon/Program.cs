using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_simon
{
    internal class Program
    {

        static void Main(string[] args)
        {
            #region Difficulty&Boardsize
            int board_x_length;
            int board_y_length;
            int bomb_amount = 0;
            string difficulty = string.Empty;
            bool no_difficulty_selected = true;
            Console.WriteLine("Velkommen til minesweeper");
            do //Selection of difficulty by totalling the amount of entries and setting the amount of bombs to 20%, 30% and 40% respectively
            {
                if (difficulty != string.Empty)
                {
                    Console.Clear();
                    Console.WriteLine("Forkert input, prøv venligst igen");
                }
                Console.WriteLine("Hvor svært skal det være? (skriv let, medium, svær)");
                Console.WriteLine("Alternativt skriv \"exit\" for at lukke dette spil");
                difficulty = Console.ReadLine().ToLower();
                switch (difficulty)
                {
                    case "let":
                        no_difficulty_selected = false;
                        break;
                    case "medium":
                        no_difficulty_selected = false;
                        break;
                    case "svær":
                        no_difficulty_selected = false;
                        break;
                    case "exit":
                        //                !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! HER SKAL LUKKEFUNKTIONEN VÆRE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        break;
                }
            } while (no_difficulty_selected); //Repeats inquiry until a valid input is entered
            do //Enables player to select height of board
            {
                Console.WriteLine("Hvor langt skal dit bræt være? (tal - f.eks. 15, minimum 10)");
                int.TryParse(Console.ReadLine(), out board_x_length);
            } while (board_x_length == 0); //Repeats inquiry until a valid input is entered
            if (board_x_length < 10) //Enforces minimum board height
            {
                board_x_length = 10;
            }
            do //Enables player to select length of board
            {
                Console.WriteLine("Hvor bredt skal dit bræt være? (tal - f.eks 30, minimum 10)");
                int.TryParse(Console.ReadLine(), out board_y_length);
            } while (board_y_length == 0); //Repeats inquiry until a valid input is entered
            if (board_y_length < 10) //Enforces minimum board length
            {
                board_y_length = 10;
            }
            switch (difficulty)
            {
                case "let":
                    bomb_amount = ((board_x_length * board_y_length) / 10) * 2;
                    break;
                case "medium":
                    bomb_amount = ((board_x_length * board_y_length) / 10) * 3;
                    break;
                case "svær":
                    bomb_amount = ((board_x_length * board_y_length) / 10) * 4;
                    break;
            }
            Console.Clear();
            #endregion
            #region Boardcreation
            int[,] board = new int[board_x_length, board_y_length];
            Random bombplacer = new Random();
            int bomb_x;
            int bomb_y;
            //Plants bombs on random parts of the board, and if there's already a bomb present, tries in another location (spools counter "i" one back)
            for (int i = 0; i < bomb_amount; i++)
            {
                bomb_x = bombplacer.Next(0, board.GetLength(0));
                bomb_y = bombplacer.Next(0, board.GetLength(1));
                //Detects if there's already a bomb present, then makes loop try again
                if (board[bomb_x, bomb_y] == 9)
                {
                    i--;
                }
                //Reduces bombclutter somewhat by making the loop try again
                else if (LocateAdjacentBombs(board, bomb_x, bomb_y) >= 6)
                {
                    i--;
                }
                else
                {
                    board[bomb_x, bomb_y] = 9;
                }
            }
            //Reads all entries in the array using the function "LocateAdjacentBombs" specified below, and sets the functions return value on the entrypoint if the entry point doesn't have a bomb (having the value "0"), return value is how many bombs are adjacent to the entry
            for (int x = 0; x < board_x_length; x++)
            {
                for (int y = 0; y < board_y_length; y++)
                {
                    if (board[x, y] == 0)
                    {
                        board[x, y] = LocateAdjacentBombs(board, x, y); //                                              !!!!!!!!!!!!!!!!!!!!!!!!!!!! SKAL FØRST KØRE EFTER SPILLEREN HAR VALGT SIT FØRSTE FELT !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    }
                }
            }
            #endregion
            //Uses an enum to replace the 0 and 9 values with bomb- and blank space indicators respectively
            Drawboard(board_x_length, board_y_length, board);
            Console.ReadLine();
        }
        /// <summary>
        /// Enum to change 9 with X and 0 to O in a visual representation
        /// </summary>
        enum Board_UI : int
        {
            X = 9,
            O = 0
        }
        /// <summary>
        /// Function to detect a specific value in "adjacent" array entries
        /// </summary>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Amount of "adjacent" fields containing a "9" (bomb)</returns>
        static int LocateAdjacentBombs(int[,] board, int x, int y)
        {
            int adjacent_bombs = 0;
            int board_x = board.GetLength(0);
            int board_y = board.GetLength(1);
            //Using a 2 by 8 array to define the location of adjacent cells
            int[,] directions =
                {
                    { -1, -1 },
                    { -1, 0 },
                    { -1, 1 },
                    { 0, -1 },
                    { 0, 1 },
                    { 1, -1 },
                    { 1, 0 },
                    { 1, 1 }
                };
            for (int i = 0; i < 8; i++)
            {
                int newX = x + directions[i, 0];
                int newY = y + directions[i, 1];
                //Makes sure that the function doesn't check outside of the arrays confinements
                if (newX >= 0 && newX < board_x && newY >= 0 && newY < board_y)
                {
                    if (board[newX, newY] == 9)
                    {
                        adjacent_bombs++;
                    }
                }
            }
            return adjacent_bombs;
        }
        /// <summary>
        /// Function to Draw the board by providing nested loop lengths and the array on which to do it
        /// </summary>
        /// <param name="x">Outer loop duration</param>
        /// <param name="y">Inner loop duration</param>
        /// <param name="board">Target array</param>
        static void Drawboard(int x, int y, int[,] board)
        {
            for (int a = 0; a < x; a++)
            {
                for (int b = 0; b < y; b++)
                {
                    Console.Write((Board_UI)board[a, b] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
