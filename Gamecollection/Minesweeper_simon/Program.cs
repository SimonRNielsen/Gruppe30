using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
    Todo:
    Lyde?
*/

namespace Minesweeper_simon
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool board_created;
            bool exit = false;
            bool loss;
            bool no_difficulty_selected = true;
            bool playing;
            bool win;
            int board_x_length;
            int board_y_length;
            int bomb_amount;
            int flag_count;
            int max_value;
            int newInput_x;
            int newInput_y;
            int score;
            //int[] highscore_values = new int[10];
            string difficulty;
            //string[] highscore_players = new string[10];
            string player;
            //string exitDirectory = Directory.GetCurrentDirectory() + @"\Gamecollection.exe";
            /*
            #region Highscore_Table
            //Checks if there's a highscore file, if not = it makes one
            if (File.Exists("highscores.txt"))
            {

            }
            else
            {
                for (int i = 0; i < highscore_players.Length; i++)
                {
                    highscore_players[i] = "Player";
                    highscore_values[i] = i + 1;
                }
                using (StreamWriter writer = new StreamWriter("highscores.txt"))
                {
                    // Write string array
                    writer.WriteLine("Strings:");
                    foreach (var str in highscore_players)
                    {
                        writer.WriteLine(str);
                    }
                    writer.WriteLine("Integers:");
                    // Write integer array
                    foreach (var num in highscore_values)
                    {
                        writer.WriteLine(num);
                    }
                }
            }
            #endregion
            */
            Console.WriteLine("Velkommen til minestryger");
            Console.WriteLine("Skriv venligst dit navn");
            player = Console.ReadLine();
            //Loops until player chooses to exit the game
            while (!exit)
            {
                //Reset relevant variables in preparation for a new game
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
                //Selection of difficulty by totalling the amount of entries and setting the amount of bombs to 10%, 20% and 30% respectively
                do
                {
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
                        default:
                            Console.WriteLine("Forkert input, prøv venligst igen");
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
                //Makes a timestamp for later comparison
                //DateTime startTime = DateTime.Now;
                //Loops gameplay cycle
                while (playing)
                {
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
                    max_value = 0;
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
                            if (wx == newInput_x && bx == newInput_y && (playerboard[newInput_x, newInput_y] == 0))
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (wx == newInput_x && bx == newInput_y)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                            }
                            Console.Write((Board_UI_simon)playerboard[wx, bx] + " ");
                        }
                        Console.WriteLine();
                    }
                    //(Re)sets colors to standard
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine($"Der er i alt {bomb_amount} bomber på brættet");
                    Console.WriteLine($"Du har markeret {flag_count} potentielle bomber");
                    Console.WriteLine();
                    Console.WriteLine("Alle felter uden bomber skal være afdækket og alle bomber SKAL være markeret med et flag (markeres med f) for at vinde");
                    Console.WriteLine("Space eller enter interager med valgte felt, WASD eller piltaster flytter markøren, \"O\" åbner alle felter omkring et tomt felt");
                    Console.WriteLine("Hvis du ikke har lyst til at spille længere kan du trykke på escape");
                    #endregion
                    //User input
                    #region PlayerInput
                    PlayerInput_simon(out int input_x, out int input_y, out exit, out bool flag, out bool input, out bool cleanup);
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
                        if (!board_created && playerboard[newInput_x, newInput_y] > 10)
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
                                else if (CountAdjacentBombs_simon(board, bomb_x, bomb_y) > 5)
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
                            //Reads all entries in the array using the function "CountAdjacentBombs_simon" specified below, and sets the functions return value on the entrypoint if the entry point doesn't have a bomb (having the value "0"), return value is how many bombs are adjacent to the entry
                            for (int x = 0; x < board_x_length; x++)
                            {
                                for (int y = 0; y < board_y_length; y++)
                                {
                                    if (board[x, y] == 0)
                                    {
                                        board[x, y] = CountAdjacentBombs_simon(board, x, y);
                                    }
                                }
                            }
                            //Prevents new board from being created until a new game is triggered
                            board_created = true;
                        }
                        #endregion
                        //Safeguards player from involuntary triggering a flag
                        if (playerboard[newInput_x, newInput_y] == 10)
                        {
                            //Do nothing
                        }
                        //Detects if player triggered a "mine/bomb"
                        else if (board[newInput_x, newInput_y] == 9)
                        {
                            loss = true;
                        }
                        //Detects if player hit a tile with no adjacent bombs and triggers a cascade to reveal all other adjacent tiles (same as the cleanup function below but should trigger automaticly)
                        //Uses same foundation as the "CountAdjacentBombs_simon" function
                        else if (board[newInput_x, newInput_y] == 0)
                        {
                            playerboard[newInput_x, newInput_y] = board[newInput_x, newInput_y];
                            for (int i = 0; i < 10; i++)
                            {
                                int playerboard_x = playerboard.GetLength(0);
                                int playerboard_y = playerboard.GetLength(1);
                                int[,] adjacent = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
                                for (int cleanx = 0; cleanx < playerboard_x; cleanx++)
                                {
                                    for (int cleany = 0; cleany < playerboard_y; cleany++)
                                    {
                                        for (int d = 0; d < 8; d++)
                                        {
                                            if (playerboard[cleanx, cleany] == 0)
                                            {
                                                int newX = cleanx + adjacent[d, 0];
                                                int newY = cleany + adjacent[d, 1];
                                                if (newX >= 0 && newX < playerboard_x && newY >= 0 && newY < playerboard_y)
                                                {
                                                    playerboard[newX, newY] = board[newX, newY];
                                                }
                                            }
                                        }

                                    }
                                }
                            }
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
                        else if (playerboard[newInput_x, newInput_y] == 11)
                        {
                            playerboard[newInput_x, newInput_y]--;
                        }
                        else
                        {
                            //Do nothing
                        }
                    }
                    //Triggers all tiles around a tile with no adjacent bombs
                    else if (cleanup)
                    {
                        int playerboard_x = playerboard.GetLength(0);
                        int playerboard_y = playerboard.GetLength(1);
                        //Using a 2 by 8 array to define the location of adjacent cells
                        int[,] adjacent = { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
                        for (int cleanx = 0; cleanx < playerboard_x; cleanx++)
                        {
                            for (int cleany = 0; cleany < playerboard_y; cleany++)
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    if (playerboard[cleanx, cleany] == 0)
                                    {
                                        int newX = cleanx + adjacent[i, 0];
                                        int newY = cleany + adjacent[i, 1];
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
                    //End-of-session scenarios
                    #region Win/loss/goodbye
                    //Displays loss message and board with all bombs revealed
                    if (loss)
                    {
                        Console.Clear();
                        DrawBoard_simon(board_x_length, board_y_length, board);
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
                    //Measures difference in time
                    //TimeSpan timer = DateTime.Now - startTime;
                    DrawBoard_simon(board_x_length, board_y_length, board);
                    Console.WriteLine("Du vandt! Du undgik alle bomberne og fandt de sikre felter!");
                    Console.WriteLine("Kunne du tænke dig at prøve igen tryk escape eller \"n\" for nej, ellers tryk på en anden tast");
                    //int timeused = (int)timer.TotalSeconds;
                    score = ((board_x_length * board_y_length)) * (bomb_amount * 2);
                    Console.WriteLine($"Din score er {score}");
                    /*
                    Console.WriteLine();
                    Console.WriteLine("Top 9 highscores plus din");
                    //////////////////////////////  \/ GENERERET AF CHATGPT \/   ///////////////////////////////////////
                    List<string> stringList = new List<string>();
                    List<int> intList = new List<int>();
                    bool readingStrings = true;
                    foreach (var line in File.ReadLines("highscores.txt"))
                    {
                        if (line == "Strings:")
                        {
                            readingStrings = true;
                            continue;
                        }
                        else if (line == "Integers:")
                        {
                            readingStrings = false;
                            continue;
                        }
                        if (readingStrings)
                        {
                            stringList.Add(line);
                        }
                        else
                        {
                            if (int.TryParse(line, out int number))
                            {
                                intList.Add(number);
                            }
                        }
                    }
                    // Convert lists to arrays
                    highscore_players = stringList.ToArray();
                    highscore_values = intList.ToArray();
                    //////////////////////////////// /\ GENERERET AF CHATGPT /\   //////////////////////////////////////
                    highscore_values[9] = score;
                    highscore_players[9] = player;
                    //Variable that "combines" the string and integer array to their respective index
                    var combiner = new (int highscore_values, string highscore_players)[highscore_values.Length];
                    //Organises players after highest score and displays them as a top 10
                    for (int i = 0; i < highscore_values.Length; i++)
                    {
                        combiner[i] = (highscore_values[i], highscore_players[i]);
                    }
                    //Compares the numeric values of highscores and sorts them from high to low
                    Array.Sort(combiner, (x, y) => y.highscore_values.CompareTo(x.highscore_values));
                    //Splits the arrays back up
                    for (int i = 0; i < highscore_values.Length; i++)
                    {
                        highscore_values[i] = combiner[i].highscore_values;
                        highscore_players[i] = combiner[i].highscore_players;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(highscore_players[i] + "   " + highscore_values[i]);
                    }
                    //////////////////////////////  \/ GENERERET AF CHATGPT \/   ///////////////////////////////////////
                    // Not part of assignment
                    using (StreamWriter writer = new StreamWriter("highscores.txt"))
                    {
                        // Write string array
                        writer.WriteLine("Strings:");
                        foreach (var str in highscore_players)
                        {
                            writer.WriteLine(str);
                        }
                        writer.WriteLine("Integers:");
                        // Write integer array
                        foreach (var num in highscore_values)
                        {
                            writer.WriteLine(num);
                        }
                    }
                    //////////////////////////////// /\ GENERERET AF CHATGPT /\   //////////////////////////////////////
                    */
                    //Registers input from user
                    var keyInput = Console.ReadKey(intercept: true);
                    switch (keyInput.Key)
                    {
                        case ConsoleKey.N:
                            exit = true;
                            Console.WriteLine("Tak fordi du gad at spille :), kom snart igen!");
                            Console.WriteLine("Tryk en tast for at lukke...");
                            Console.ReadKey();
                            break;
                        case ConsoleKey.Escape:
                            exit = true;
                            Console.WriteLine("Tak fordi du gad at spille :), kom snart igen!");
                            Console.WriteLine("Tryk en tast for at lukke...");
                            Console.ReadKey();
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            //Process.Start(exitDirectory);
        }
        /// <summary>
        /// Enum to change 9 with X and 0 to O in a visual representation
        /// </summary>
        enum Board_UI_simon : int
        {
            X = 9,  //Bomb(Mines)
            o = 0,  //Known/opened til with no adjacent bombs
            F = 10, //Flag
            O = 11  //Unknown/unopened tile
        }
        /// <summary>
        /// Function to detect a specific value in "adjacent" array entries
        /// </summary>
        /// <param name="board">Array representing the board</param>
        /// <param name="x">x-coordinate on board</param>
        /// <param name="y">y-coordinate on board</param>
        /// <returns>Amount of "adjacent" fields containing a "9" (bomb)</returns>
        static int CountAdjacentBombs_simon(int[,] board, int x, int y)
        {
            int adjacent_bombs = 0;
            int board_x = board.GetLength(0);
            int board_y = board.GetLength(1);
            //Using a 2 by 8 array to define the location of adjacent cells
            int[,] adjacent =
                {
                    { -1, -1 }, //Up + left
                    { -1, 0 }, //Up
                    { -1, 1 }, //Up + right 
                    { 0, -1 }, //Left
                    { 0, 1 }, //Right
                    { 1, -1 }, //Down + left
                    { 1, 0 }, //Down
                    { 1, 1 } //Down + right
                };
            for (int i = 0; i < 8; i++)
            {
                int newX = x + adjacent[i, 0];
                int newY = y + adjacent[i, 1];
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
        /// Also colorcodes background and numbers according to preset values
        /// </summary>
        /// <param name="x">Outer loop duration</param>
        /// <param name="y">Inner loop duration</param>
        /// <param name="board">Target array</param>
        static void DrawBoard_simon(int x, int y, int[,] board)
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
                    Console.Write((Board_UI_simon)board[a, b] + " ");
                }
                Console.WriteLine();
            }
            //(Re)sets colors to standard
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// Function to record and act on player input
        /// </summary>
        static void PlayerInput_simon(out int x, out int y, out bool exit, out bool flag, out bool input, out bool cleanup)
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
