﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper_simon
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool no_difficulty_selected = true;
            bool exit = false;
            bool playing;
            bool win;
            bool loss;
            bool board_created;
            int board_x_length;
            int board_y_length;
            int newInput_x;
            int newInput_y;
            int bomb_amount;
            int flag_count;
            int max_value;
            string difficulty;
            Console.WriteLine("Velkommen til minesweeper");
            //Loops until player chooses to exit the game
            while (exit == false)
            {
                #region Difficulty&Boardsize
                win = false;
                loss = false;
                playing = true;
                newInput_x = 0;
                newInput_y = 0;
                bomb_amount = 0;
                difficulty = string.Empty;
                board_created = false;
                Console.Clear();
                //Selection of difficulty by totalling the amount of entries and setting the amount of bombs to 20%, 30% and 40% respectively
                do
                {
                    if (difficulty != string.Empty)
                    {
                        Console.Clear();
                        Console.WriteLine("Forkert input, prøv venligst igen");
                    }
                    Console.WriteLine("Hvor svært skal det være? (skriv let, medium, svær)");
                    Console.WriteLine("Alternativt skriv \"exit\" for at lukke dette spil");
                    difficulty = Console.ReadLine().ToLower();
                    //Switch case to react on user input
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
                            no_difficulty_selected = false;
                            exit = true;
                            Console.WriteLine("Tak fordi du gad at spille, kom snart igen!");
                            Console.WriteLine("Tryk en tast for at lukke...");
                            Console.ReadKey();
                            break;
                    }
                    //Repeats inquiry until a valid input is entered
                } while (no_difficulty_selected);
                //Checks if user wants to close the game then breaks the gameplay loop
                if (exit)
                {
                    break;
                }
                //Enables player to select height of and repeats inquiry until a valid input is entered
                do
                {
                    Console.WriteLine("Hvor langt skal dit bræt være? (tal - f.eks. 15, minimum 10, maximum 25)");
                    int.TryParse(Console.ReadLine(), out board_x_length);
                } while (board_x_length == 0);
                //Enforces minimum and maximum board height
                if (board_x_length < 10)
                {
                    board_x_length = 10;
                }
                else if (board_x_length > 25)
                {
                    board_x_length = 25;
                }
                //Enables player to select length of board and repeats inquiry until a valid input is entered
                do
                {
                    Console.WriteLine("Hvor bredt skal dit bræt være? (tal - f.eks 30, minimum 10, maximum 50)");
                    int.TryParse(Console.ReadLine(), out board_y_length);
                } while (board_y_length == 0);
                //Enforces minimum and maximum board length
                if (board_y_length < 10)
                {
                    board_y_length = 10;
                }
                else if (board_y_length > 50)
                {
                    board_y_length = 50;
                }
                //Creates an array with input x,y value size
                int[,] board = new int[board_x_length, board_y_length];
                //Sets amount of bombs to plant based on earlier difficulty setting and "size" of the board (array)
                switch (difficulty)
                {
                    case "let":
                        bomb_amount = ((board_x_length * board_y_length) / 10) * 1;
                        break;
                    case "medium":
                        bomb_amount = ((board_x_length * board_y_length) / 10) * 2;
                        break;
                    case "svær":
                        bomb_amount = ((board_x_length * board_y_length) / 10) * 3;
                        break;
                }
                #endregion
                //User interface, used an enum to convert integers into letters
                #region User_Interface
                //Creates a clean board
                int[,] playerboard = new int[board_x_length, board_y_length];
                for (int px = 0; px < board_x_length; px++)
                {
                    for (int py = 0; py < board_y_length; py++)
                    {
                        playerboard[px, py] = 11;
                    }
                }
                //Loops current game cycle
                DateTime startTime = DateTime.Now;
                while (playing)
                {
                    max_value = 0;
                    //Clears console to avoid duplicates of the board, and as such, a cleaner and simple interface
                    Console.Clear();
                    //Counts the amount of flags marked for every loop
                    flag_count = 0;
                    foreach (int flag_counter in playerboard)
                    {
                        if (flag_counter == 10)
                        {
                            flag_count++;
                        }
                    }
                    //Detects if you've won by comparing flagged mines
                    for (int win_x = 0; win_x < board_x_length; win_x++)
                    {
                        for (int win_y = 0; win_y < board_y_length; win_y++)
                        {
                            if (playerboard[win_x, win_y] > 10)
                            {
                                max_value = playerboard[win_x, win_y];
                            }
                        }
                    }
                    if (max_value < 11 && bomb_amount == flag_count)
                    {
                        win = true;
                        break;
                    }
                    else
                    {
                        win = false;
                    }
                    //Draws board, indicates by color where the players marker is
                    for (int wx = 0; wx < board_x_length; wx++)
                    {
                        for (int bx = 0; bx < board_y_length; bx++)
                        {
                            //Detects if the players marker is at the same coordinate that the nested loop is drawing, and if so colors it to highlight it
                            if ((wx == newInput_x) && (bx == newInput_y))
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                            }
                            //Colorcodes numbers and characters
                            switch (playerboard[wx, bx])
                            {
                                case 0:
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    break;
                                case 5:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                case 6:
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case 7:
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    break;
                                case 8:
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                case 9:
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                case 10:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case 11:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }
                            Console.Write((Board_UI)playerboard[wx, bx] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine($"Der er {bomb_amount} bomber på brættet, du har markeret gættet på at {flag_count} er det");
                    Console.WriteLine("Alle felter uden bomber skal være afdækket og alle bomber SKAL være markeret med et flag (markeres med f) for at vinde");
                    Console.WriteLine("Space eller enter interager med valgte felt, WASD eller piltaster flytter markøren, \"O\" åbner alle felter omkring et \"o\"");
                    #endregion
                    //User input
                    #region PlayerInput
                    PlayerInput(out int input_x, out int input_y, out exit, out bool flag, out bool input, out bool cleanup);
                    //Adds(or subtracts) user input to another integer so it doesn't get overridden
                    newInput_x += input_x;
                    newInput_y += input_y;
                    //Ensures userinput stays inside bounds
                    if (newInput_x < 0)
                    {
                        newInput_x = 0;
                    }
                    if (newInput_y < 0)
                    {
                        newInput_y = 0;
                    }
                    if (newInput_x > board_x_length - 1)
                    {
                        newInput_x = board_x_length - 1;
                    }
                    if (newInput_y > board_y_length - 1)
                    {
                        newInput_y = board_y_length - 1;
                    }
                    //Detects if the player wants to trigger a certain tile
                    if (input)
                    {
                        //Timer to keep track of elapsed time
                        #region Boardcreation
                        //Creates a board after the player makes his first selection, ensuring that a bomb can't be triggered as the first tile
                        if (!board_created)
                        {

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
                                //Prevents game from planting a bomb on the starting tile
                                else if ((bomb_x == newInput_x) && (bomb_y == newInput_y))
                                {
                                    i--;
                                }
                                //Actual bomb planting
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
                                        board[x, y] = LocateAdjacentBombs(board, x, y);
                                    }
                                }
                            }
                            //Prevents new board from being created until a new game is triggered
                            board_created = true;
                            #endregion
                        }
                        //Safeguards player from involuntary triggering a flag
                        else if (playerboard[newInput_x, newInput_y] == 10)
                        {
                            
                        }
                        //Detects if player triggered a "mine/bomb"
                        else if (board[newInput_x, newInput_y] == 9)
                        {
                            loss = true;
                        }
                        //Reveals underlying number of adjacent bombs
                        else
                        {
                            playerboard[newInput_x, newInput_y] = board[newInput_x, newInput_y];
                        }
                    }
                    //Toggles flag marker
                    else if (flag)
                    {
                        if (playerboard[newInput_x, newInput_y] == 10)
                        {
                            playerboard[newInput_x, newInput_y]++;
                        }
                        else
                        {
                            playerboard[newInput_x, newInput_y]--;
                        }
                    }
                    //Triggers all tiles around a tile with no adjacent bombs
                    else if (cleanup)
                    {
                        int playerboard_x = playerboard.GetLength(0);
                        int playerboard_y = playerboard.GetLength(1);
                        //Using a 2 by 8 array to define the location of adjacent cells
                        int[,] directions = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
                        for (int cleanx = 0; cleanx < playerboard_x; cleanx++)
                        {
                            for (int cleany = 0; cleany < playerboard_y; cleany++)
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    if (playerboard[cleanx, cleany] == 0)
                                    {
                                        int newX = cleanx + directions[i, 0];
                                        int newY = cleany + directions[i, 1];
                                        //Makes sure that the function doesn't check outside of the arrays confinements
                                        if (newX >= 0 && newX < playerboard_x && newY >= 0 && newY < playerboard_y)
                                        {
                                            playerboard[newX, newY] = board[newX, newY];
                                        }
                                    }
                                }

                            }
                        }
                    }
                    #endregion
                    #region Win/loss/goodbye
                    //Displays loss message and board with all bombs revealed
                    if (loss)
                    {
                        Console.Clear();
                        DrawBoard(board_x_length, board_y_length, board);
                        Console.WriteLine("Du ramte en bombe og har derfor tabt :(");
                        Console.WriteLine("Kunne du tænke dig at prøve igen tryk escape eller \"n\" for nej, ellers tryk på en anden tast");
                        var keyInput = Console.ReadKey(intercept: true);
                        switch (keyInput.Key)
                        {
                            case ConsoleKey.N:
                                exit = true;
                                playing = false;
                                break;
                            case ConsoleKey.Escape:
                                exit = true;
                                playing = false;
                                break;
                            default:
                                playing = false;
                                break;
                        }
                    }
                    //Displays goodbye message
                    if (exit)
                    {
                        playing = false;
                        Console.WriteLine("Tak fordi du gad at spille :), kom snart igen!");
                        Console.WriteLine("Tryk en tast for at lukke...");
                        Console.ReadKey();
                    }
                }
                //Displays win message and board with all bombs revealed
                if (win)
                {
                    Console.Clear();
                    TimeSpan timer = DateTime.Now - startTime;
                    DrawBoard(board_x_length, board_y_length, board);
                    Console.WriteLine("Du vandt! Du undgik alle bomberne og fandt de sikre felter!");
                    Console.WriteLine("Kunne du tænke dig at prøve igen tryk escape eller \"n\" for nej, ellers tryk på en anden tast");
                    Console.WriteLine($"Du brugte {timer.TotalSeconds} sekunder");
                    var keyInput = Console.ReadKey(intercept: true);
                    switch (keyInput.Key)
                    {
                        case ConsoleKey.N:
                            exit = true;
                            break;
                        case ConsoleKey.Escape:
                            exit = true;
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// Enum to change 9 with X and 0 to O in a visual representation
        /// </summary>
        enum Board_UI : int
        {
            X = 9,
            o = 0,
            F = 10,
            O = 11
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
        static void DrawBoard(int x, int y, int[,] board)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            for (int a = 0; a < x; a++)
            {
                for (int b = 0; b < y; b++)
                {
                    switch (board[a, b])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 8:
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case 9:
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                    }
                    Console.Write((Board_UI)board[a, b] + " ");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Function to record and act on player input
        /// </summary>
        static void PlayerInput(out int x, out int y, out bool exit, out bool flag, out bool input, out bool cleanup)
        {
            x = 0;
            y = 0;
            exit = false;
            flag = false;
            input = false;
            cleanup = false;
            var keyInput = Console.ReadKey(intercept: true);
            switch (keyInput.Key)
            {
                case ConsoleKey.LeftArrow:
                    y--;
                    break;
                case ConsoleKey.RightArrow:
                    y++;
                    break;
                case ConsoleKey.UpArrow:
                    x--;
                    break;
                case ConsoleKey.DownArrow:
                    x++;
                    break;
                case ConsoleKey.A:
                    y--;
                    break;
                case ConsoleKey.D:
                    y++;
                    break;
                case ConsoleKey.W:
                    x--;
                    break;
                case ConsoleKey.S:
                    x++;
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                case ConsoleKey.Enter:
                    input = true;
                    break;
                case ConsoleKey.Spacebar:
                    input = true;
                    break;
                case ConsoleKey.F:
                    flag = true;
                    break;
                case ConsoleKey.O:
                    cleanup = true;
                    break;
                default:
                    break;
            }
        }
    }
}
