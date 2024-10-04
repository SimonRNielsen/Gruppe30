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
            string[] games = new string[4] { "Minestryger", "Jeopardy", "Sænke Slagskibe", "Mastermind" };
            /* Strings with path to game executables, which are added to same basefolder as "Gamecollection.exe" by referencing it in the project
            string minesweeper = Directory.GetCurrentDirectory() + @"\Minesweeper_simon.exe";
            string jeopardy = Directory.GetCurrentDirectory() + @"\Jeopardy_Irene.exe";
            string battleships = Directory.GetCurrentDirectory() + @"\Rikke.exe";
            string mastermind = Directory.GetCurrentDirectory() + @"\Philip_Mastermind.exe";
            */
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
                Console.ResetColor();
                //Calls Player input function
                PlayerInput_Main(out marker, out exit, out input);
                //Starts 1 of 4 games up depending on player selection
                if (input)
                {
                    switch (newMarker)
                    {
                        case 0:
                            Console.Clear();
                            Minesweeper_Simon();
                            //Process.Start(minesweeper);
                            break;
                        case 1:
                            Console.Clear();
                            Jeopardy_Irene();
                            //Process.Start(jeopardy);
                            break;
                        case 2:
                            Console.Clear();
                            //Process.Start(battleships);
                            break;
                        case 3:
                            Console.Clear();
                            Philip_MasterMind();
                            //Process.Start(mastermind);
                            break;
                    }
                }
            }
            //Loops selection progress
            while (!exit);
            //Displays farewell message if no program was initiated

            Console.WriteLine("Tak fordi du gad at spille med os!");
            Console.WriteLine("Hilsen Irene, Rikke, Philip og Simon");
            Console.WriteLine("Tryk på en tast for at afslutte");
            Console.ReadKey();
        }
        /// <summary>
        /// Detects playerinput
        /// </summary>
        /// <param name="x">Changes movement on x-axis</param>
        /// <param name="exit">Logs "escape" being pressed</param>
        /// <param name="input">Logs "enter" being pressed</param>
        static void PlayerInput_Main(out int x, out bool exit, out bool input)
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
        /// <summary>
        /// Minesweeper lavet af Simon N
        /// </summary>
        static void Minesweeper_Simon()
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
            string difficulty;
            /*
            string player;
            //Arrays for use with highscores
            int[] highscore_values = new int[10];
            string[] highscore_players = new string[10];
            //String defining the executable for main project
            string exitDirectory = Directory.GetCurrentDirectory() + @"\Gamecollection.exe";
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
            Console.WriteLine("Velkommen til minestryger, tryk en tast for at fortsætte");
            Console.ReadLine();
            /*
            Console.WriteLine("Skriv venligst dit navn");
            player = Console.ReadLine();
            */
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
                            Console.Write((Board_UI_Simon)playerboard[wx, bx] + " ");
                        }
                        Console.WriteLine();
                    }
                    //Resets colors to standard
                    Console.ResetColor();
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
                    PlayerInput_Simon(out int input_x, out int input_y, out exit, out bool flag, out bool input, out bool cleanup);
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
                                else if (CountAdjacentBombs_Simon(board, bomb_x, bomb_y) > 5)
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
                            //Reads all entries in the array using the function "CountAdjacentBombs_Simon" specified below, and sets the functions return value on the entrypoint if the entry point doesn't have a bomb (having the value "0"), return value is how many bombs are adjacent to the entry
                            for (int x = 0; x < board_x_length; x++)
                            {
                                for (int y = 0; y < board_y_length; y++)
                                {
                                    if (board[x, y] == 0)
                                    {
                                        board[x, y] = CountAdjacentBombs_Simon(board, x, y);
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
                        //Uses same foundation as the "CountAdjacentBombs_Simon" function
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
                        DrawBoard_Simon(board_x_length, board_y_length, board);
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
                    DrawBoard_Simon(board_x_length, board_y_length, board);
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
        /// Enum to change numbers to characters in a visual representation
        /// </summary>
        enum Board_UI_Simon : int
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
        static int CountAdjacentBombs_Simon(int[,] board, int x, int y)
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
        static void DrawBoard_Simon(int x, int y, int[,] board)
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
                    Console.Write((Board_UI_Simon)board[a, b] + " ");
                }
                Console.WriteLine();
            }
            //(Re)sets colors to standard
            Console.ResetColor();
        }
        /// <summary>
        /// Function to record and act on player input
        /// </summary>
        static void PlayerInput_Simon(out int x, out int y, out bool exit, out bool flag, out bool input, out bool cleanup)
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
        enum Farver { rød, blå, gul, grøn, lilla, brun }
        static void Philip_MasterMind()
        {
            byte[] kodeTal = new byte[4]; //Array der skal indeholde 4 tilfældigt generede tal, der skal bruges til gætte-koden.
            Farver[] kodeFarver = new Farver[kodeTal.Length];//Enum array, der indeholder de fire farver der udgør koden. 
            Farver[] spillerGaet = new Farver[kodeTal.Length];//Array, der skal indeholde spillerens gæt.
            Random rnd = new Random();
            int runde; //Variabel der gemmer hvad runde vi er i
            string highscore; //Variabel der gemmer highscore (skal indeholde Navn + runde)
            int highscoreRunde = 11; //Variabel der gemmer den highscore runden. 11, fordi der er 10 runder.
            bool stemningForSpil = true; //Bruges til at gemme om spilleren vil fortsætte 
            string inSvar; //Variabel der gemmer spillerens input, så de kan valideres
            int rigtigFarve = 0; //Variabel der skal gemme antal farver, uden rigtig plads, som spilleren har gættet
            int rigtigPlace = 0; //Variabel der skal gemme antal farver, på den rigtige plads, som spilleren har gættet
            bool[] placeBrugt = new bool[kodeTal.Length]; //Array der bruges til gemme om en plads en plads i koden allerede er gættet
            bool[] gaetBrugt = new bool[kodeTal.Length]; //Array der bruges til gemme om en plads en plads i spiller gæt allerede er brugt
            Farver[,] tidlGaet = new Farver[11, 4];//Array til at gemme spillerens gæt fra tidligere runder.
            int[,] tidlRes = new int[11, 2]; //Array til at gemme sorte/hvide fra tidligere runder

            do //Loop, der egentlig bare tager højde for om spilleren stadig vil spille.
            {
                Velkomst();//Funktion til velkosmtbesked, fordi den er så lang.
                //For loop, der genererer random tal, på hver plads i kodeTal array
                for (byte i = 0; i < kodeTal.Length; i++)
                {
                    kodeTal[i] = (byte)rnd.Next(0, 6);
                }
                //Loop der indsætter de fire farver (fra tal i kodeTal) i kodeFarver arrayet
                for (int i = 0; i < kodeFarver.Length; i++)
                {
                    kodeFarver[i] = (Farver)kodeTal[i];
                }
                runde = 1; /*Sætter runde til 1. Man vender kun tilbage til dette loop,
                            hvis man har vundet, eller tabt, og valgt at fortsætte eææer afsluttet spillet */
                /*
                 //Udskriver den kode der skal gættes til konsollen:
                 foreach (Farver farve in kodeFarver)
                     Console.WriteLine(farve);
                */

                Console.WriteLine("\nLad os først starte med dit navn. Hvad vil du gerne kaldes?");
                string spillerNavn = Console.ReadLine();
                if (spillerNavn.ToLower() == "exit") //Giver mulighed for at vende tilbage til fælles menu
                    break;
                Console.WriteLine($"\nHej med dig {spillerNavn}!\n" +
                    $"Tryk på en vilkårlig tast, for at begynde spillet! Eller tryk Escape for at vende tilbage til menuen.");
                var tast = Console.ReadKey();
                if (tast.Key == ConsoleKey.Escape) //Giver mulighed for at vende tilbage til fælles menu med esc tryk
                    break;

                while (runde < 11) //Loop med selve spille, fra runde 1-10
                {
                    Console.Clear(); //Sletter konsollens indhold. BEMÆRK! KUN EN HVIS DEL, SVARENDE TIL HVAD DER KAN VÆRE I ET VINDUE!
                    if (runde > 1) //Skriver spillerens tidligere gæt og resultater for foregående runder, hvis nuvræende runde >1.
                    {
                        Philip_SkrivCyan("Tidligere gæt: ");
                        for (int i = 1; i < runde; i++)
                        {
                            Console.WriteLine("Runde " + i);
                            for (int j = 0; j < tidlGaet.GetLength(1); j++)
                            {
                                Philip_SkrivFarve(tidlGaet[i, j], Convert.ToString(tidlGaet[i, j]) + " ");
                            }
                            Console.WriteLine($"Du havde {tidlRes[i, 0]} sorte brikker og {tidlRes[i, 1]} hvide brikker.\n");
                        }
                    }
                    Philip_SkrivCyan("Runde " + runde);
                    for (int i = 0; i < kodeTal.Length; i++) //Loop hvor spilleren indtaster sine 4 (kodeTal.Length) gæt. 
                    {
                        Console.WriteLine("Hvilken farve gætter du på plads " + (i + 1) + "?" +
                            "\nHusk at du kan vælge imellem: Rød, Blå, Gul, Grøn, Lilla, Brun");
                        inSvar = Philip_GodkendSvar(); //Funktion der beder om input, kontrollerer at det er gyldigt og returere et gyldigt svar som string
                        spillerGaet[i] = (Farver)Enum.Parse(typeof(Farver), inSvar); //Svaret gemmes i array med spillerens gæt
                    }
                    Console.Clear();
                    Philip_SkrivCyan("Dit gæt er:\n");
                    for (int i = 0; i < kodeTal.Length; i++)
                    {
                        Philip_SkrivFarve(spillerGaet[i], spillerGaet[i] + " ");
                    }

                    //If statement, der tjekker om spilleren har vundet ved at gætte koden
                    if (spillerGaet[0] == kodeFarver[0] &&
                        spillerGaet[1] == kodeFarver[1] &&
                        spillerGaet[2] == kodeFarver[2] &&
                        spillerGaet[3] == kodeFarver[3])
                    {
                        Console.Clear();
                        //Besked med tillykke og score
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nTillykke du svarede rigtigt! \n" +
                            "Koden var: ");
                        Philip_SkrivFarve(kodeFarver[0], kodeFarver[0] + " ");
                        Philip_SkrivFarve(kodeFarver[1], kodeFarver[1] + " ");
                        Philip_SkrivFarve(kodeFarver[2], kodeFarver[2] + " ");
                        Philip_SkrivFarve(kodeFarver[3], kodeFarver[3] + " ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nDu gjorde på det på " + runde + " runder!");
                        if (runde < highscoreRunde) //Tjekker om man har slået highscoren
                        {
                            Console.WriteLine("Du har slået highscoren!");
                            highscore = "Highscoren er: " + Convert.ToString(runde) + ", den blev sat af: " + spillerNavn;
                            highscoreRunde = runde;
                            Console.WriteLine(highscore);
                        }
                        Console.ResetColor();
                        stemningForSpil = Philip_Afslut(); //Tjekker om de vil afslutte spillet
                        break; //Breaker ud, så man ender i "do...while" loopet
                    }
                    else //Håndtering af hvis spilleren ikke vandt med sit gæt
                    {
                        Console.WriteLine("\n\nDu gættede ikke rigtigt.\n");
                        //Loop, der kontrollere om spillerens gæt svarer til rigtige farver med rigtig palcering (hvid brik)
                        for (int i = 0; i < kodeFarver.Length; i++)
                        {
                            if (kodeFarver[i] == spillerGaet[i] && !placeBrugt[i])
                            {
                                rigtigPlace++;
                                placeBrugt[i] = true;
                                gaetBrugt[i] = true;
                            }
                        }
                        //Loop der kontrollerer om gættet svarer til en farve uden rigtig placering(sort brik)
                        //Hvid-loopet er kørt, så alle pladser hvor gættet er rigtigt placeret, er allerede udfyldt.
                        //Hvis gættet allerede har gættet en hvid, tælles det heller ikke igen.
                        for (int i = 0; i < spillerGaet.Length; i++)
                        {
                            for (int j = 0; j < kodeFarver.Length; j++)
                            {
                                if (spillerGaet[i] == kodeFarver[j] && !placeBrugt[j] && !gaetBrugt[i])
                                {
                                    rigtigFarve++;
                                    placeBrugt[j] = true;
                                    break;
                                }
                            }

                        }
                        //Feedback til spilleren, der svarer til de sorte/hvide brikker i klassisk MasterMind.
                        Console.WriteLine($"Du har {rigtigFarve} sort(e) brik(ker). Det er farve(r) der optræder i koden, men på den forkerte plads\n" +
                            $"og {rigtigPlace} hvid(e) brik(ker). Det er farver på den rigtige plads\n");
                        Philip_SkrivCyan("Tryk på en knap for at forsætte til næste runde.\nTryk Escape for at genstarte spillet.");
                        tast = Console.ReadKey(); //Giver mulighed for at vende tilbage til spillets startmenu
                        if (tast.Key == ConsoleKey.Escape)
                            break;
                    }
                    if (runde == 10) //Hvis vi er i runde 10, er spillet slut og spilleren spørges om de vil afslutte eller forsætte
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du nåede desværre ikke at gætte koden inden for 10 runder.");
                        Console.ResetColor();
                        stemningForSpil = Philip_Afslut();
                    }
                    tidlGaet[runde, 0] = spillerGaet[0]; //Gemmer rundens gæt og resultat i tidlGaet array, på denne rundes række.
                    tidlGaet[runde, 1] = spillerGaet[1];
                    tidlGaet[runde, 2] = spillerGaet[2];
                    tidlGaet[runde, 3] = spillerGaet[3];
                    tidlRes[runde, 0] = rigtigFarve; //Gemmer rundens resultat med sorte(hvide brikker, i tidlRes array, på denne rundes række
                    tidlRes[runde, 1] = rigtigPlace;

                    rigtigFarve = 0; //Sætter rigtigFarve til 0, så det kun er den aktuelle rundes gæt der evalueres. 
                    rigtigPlace = 0; //Sætter rigtigPlace til 0, så det kun er den aktuelle rundes gæt der evalueres.
                    for (int i = 0; i < kodeTal.Length; i++) //Resetter de brugte gæt og placeringer
                    {
                        placeBrugt[i] = false;
                        gaetBrugt[i] = false;
                    }
                    runde++;
                }
                Console.Clear(); //Cleare konsollen, når et spil er afsluttet, efter sejr eller 10 runder
            } while (stemningForSpil);
        }

        /// <summary>
        /// En funktion, der skriver en velkomstbesked ud til konsollen, med forklaring af reglerne i MasterMind.
        /// </summary>
        static void Velkomst()
        {
            Philip_SkrivCyan(@"Velkommen til MasterMind!

Her er spillets regler:

Målet er at gætte den kode, som computeren har genereret, inden for 10 runder. 
Koden består af 4 farver. Farverne kan være: Rød, Blå, Gul, Grøn, Lilla eller Brun.

For hver runde skal du gætte på hvilke 4 farver du tror der er i koden. Du gætter på en plads ad gangen.
Vær opmærksom på at hver farve godt kan optræde flere gange i samme kode. 

Hvis du har gættet en farve der optræder i koden, men som ikke er på den rette plads,
fortæller spillet dig det. Det svarer til en sort brik, i den fysiske udgave af MasterMind.
Hvis du har gættet både rigtig farve og plads, fortæller spillet dig det. Det svarer til en hvid brik, i den fysiske udgave af MasterMind.
Du får ikke at vide på hvilken plads du har gættet rigtigt. 

Spillet slutter ved at du enten har gættet den rigtige kode, eller at der er gået 10 runder.
Skriv exit for at vende tilbage til menuen.

God fornøjelse!");
        }
        /// <summary>
        /// Funktion der spørger om spilleren vil afslutte. Hvis de vil, reutrnere funktionen false. Ellers returnere den true.
        /// </summary>
        /// <returns></returns>
        static bool Philip_Afslut()
        {
            bool afslutRes = true;
            Console.WriteLine("\nØnsker du at afslutte? Hvis ja: skriv \"quit\" og tryk enter.\nHvis du vil spille igen, tryk enter");
            if (Console.ReadLine().ToLower().Trim() == "quit")
            {
                afslutRes = false;
            }
            return afslutRes;

        }
        /// <summary>
        /// Lader spilleren skrive et input, og kontroller at det er et gyldigt svar. 
        /// Returnere en string, med det gyldige svar.
        /// </summary>
        /// <returns></returns>
        static string Philip_GodkendSvar()
        {
            string svar;
            while (true)
            {
                svar = Console.ReadLine()
                    .ToLower()
                    .Trim(); //fjerner whitespaces, f.eks. hvis spilleren er kommet til at trykke mellemrum før/efter

                if (svar == "rød")
                    return svar;
                else if (svar == "blå")
                    return svar;
                else if (svar == "gul")
                    return svar;
                else if (svar == "grøn")
                    return svar;
                else if (svar == "lilla")
                    return svar;
                else if (svar == "brun")
                    return svar;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDu har ikke indtastet et gyldigt svar.\nDu skal gætte på en af de nævnte farver\nPrøv igen:");
                    Console.ResetColor();
                }
            }
        }
        /// <summary>
        /// Modtager en string som den udskriver til konsollen i farven cyan. 
        /// </summary>
        /// <param name="tekst"> Den tekst, som string, man vil have udskrevet</param>
        static void Philip_SkrivCyan(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }
        /// <summary>
        /// Modtager en farve (enum Farver) som første argument, og en tekst som string som andet argument. 
        /// Udskriver teksten i den angivne farve. 
        /// </summary>
        /// <param name="farve">Farven til teskten. Type: enum Farve</param>
        /// <param name="tekst">Den tekst der skal udskrives til konsollen.</param>
        static void Philip_SkrivFarve(Farver farve, string tekst)
        {
            switch (farve)
            {
                case Farver.blå:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.brun:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.grøn:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.gul:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.lilla:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.rød:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;

            }
        }
        static void Jeopardy_Irene()
        {
            //Jeopardy-blå
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();


            string[] rigtigSvar = new string[] { "gepard", "flagermus", "kamæleon", "due", "næbdyr", "mario", "minecraft", "link", "world of warcraft", "the binding of isaac" };
            //string rigtigSvar = "gepard"; 


            #region Intro + spiller navneArray
            Console.WriteLine("Velkommen til Jeopardy! Det er spillet hvor du stiller spørgsmålene og vi har svarene!");
            Console.WriteLine("Der kan være mellem 1-3 spillere. Hvor mange spillere er I?");

            int antalSpillere = Convert.ToInt32(Console.ReadLine());
            int[,] spillerPoint = new int[1, 3];

            string spillerEt;
            string spillerTo;
            string spillerTre;

            for (int i = 0; i < antalSpillere; i++)
            {
                if (antalSpillere > 4)
                {
                    Console.WriteLine("Der kan max være 3 spillere. Start forfra."); //Start forfra - how?
                    break;
                }

                Console.WriteLine($"Spiller {i + 1}, hvad hedder du?");

                switch (i)
                {
                    case 0:
                        spillerEt = Console.ReadLine();
                        break;
                    case 1:
                        spillerTo = Console.ReadLine();
                        break;
                    case 2:
                        spillerTre = Console.ReadLine();
                        break;

                }
            }

            Random random = new Random();
            int randomNumber = random.Next(0, antalSpillere + 1);
            #endregion

            //clear intro, gør plads til board
            Console.Clear();

            string spillerSvar = Console.ReadLine();
            int aktivSpiller = randomNumber;

            int spillerTur = 1;

            while (spillerTur == 1)
            {
                Console.WriteLine($"Det er spiller {aktivSpiller}'s tur\n");




                #region Kategori array
                string[,] kategoriArray = new string[6, 6];

                //kategorier
                kategoriArray[0, 0] = "Dyr   ";
                kategoriArray[0, 1] = "Spil  ";
                kategoriArray[0, 2] = "Film  ";
                kategoriArray[0, 3] = "Natur ";
                kategoriArray[0, 4] = "Quotes";
                kategoriArray[0, 5] = "Dumt  ";

                //spørgsmål - kategori Spil
                kategoriArray[1, 0] = "200";
                kategoriArray[1, 1] = "   200";
                kategoriArray[1, 2] = "   200";
                kategoriArray[1, 3] = "   200";
                kategoriArray[1, 4] = "   200";
                kategoriArray[1, 5] = "   200";

                //spørgsmål - kategori Film
                kategoriArray[2, 0] = "400";
                kategoriArray[2, 1] = "   400";
                kategoriArray[2, 2] = "   400";
                kategoriArray[2, 3] = "   400";
                kategoriArray[2, 4] = "   400";
                kategoriArray[2, 5] = "   400";

                //spørgsmål - kategori Natur
                kategoriArray[3, 0] = "600";
                kategoriArray[3, 1] = "   600";
                kategoriArray[3, 2] = "   600";
                kategoriArray[3, 3] = "   600";
                kategoriArray[3, 4] = "   600";
                kategoriArray[3, 5] = "   600";

                //spørgsmål - kategori Quotes
                kategoriArray[4, 0] = "800";
                kategoriArray[4, 1] = "   800";
                kategoriArray[4, 2] = "   800";
                kategoriArray[4, 3] = "   800";
                kategoriArray[4, 4] = "   800";
                kategoriArray[4, 5] = "   800";

                //spørgsmål - kategori Dumt
                kategoriArray[5, 0] = "1000";
                kategoriArray[5, 1] = "  1000";
                kategoriArray[5, 2] = "  1000";
                kategoriArray[5, 3] = "  1000";
                kategoriArray[5, 4] = "  1000";
                kategoriArray[5, 5] = "  1000";

                for (int x = 0; x < kategoriArray.GetLength(0); x++)
                {
                    for (int y = 0; y < kategoriArray.GetLength(1); y++)
                    {
                        Console.Write(kategoriArray[x, y] + " ");

                    }
                    //breaker loops op så vi får rækker i boardet
                    Console.Write("\n----------------------------------------\n");
                }
                #endregion

                Console.WriteLine("Hvilken kategori vælger du?");
                string spillerKategoriValg = Console.ReadLine().ToLower();


                #region switch med spørgsmål
                #region dyr
                //Her vælges spørgsmål, da det er en "opremsning" har jeg brugt en switch
                if (spillerKategoriValg == "dyr")
                {

                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());


                    switch (spillerVærdiValg) //Dyr
                    {
                        case 200:
                            Console.WriteLine("Kendt for at være det hurtigste dyr i verden\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[0]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[0])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[0]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 400:
                            Console.WriteLine("Det eneste pattedyr der kan flyve\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[1]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[1])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[1]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 600:
                            Console.WriteLine("Camouflerer sig ved at skifte farve til sine omgivelser\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[2]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[2])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[2]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 800:
                            Console.WriteLine("Kendt for at være symbol på fred, ofte afbildet med en kvist i munden\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[3]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[3])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[3]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 1000:
                            Console.WriteLine("'Laver ikke så meget', men er nemesis til Doofenshmirtz\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[4]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[4])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[4]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            break;
                    }
                }
                #endregion
                #region spil
                if (spillerKategoriValg == "spil")
                {

                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                    switch (spillerVærdiValg) //spil
                    {
                        case 200:
                            Console.WriteLine("Hans prinsesse er i et andet slot\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[5]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[5])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[5]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 400:
                            Console.WriteLine("Et spil hvor det er cool to be square - for alt er firkantet\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[6]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[6])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[6]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 600:
                            Console.WriteLine("Ingen koroks kan gemme sig for ham\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[7]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[7])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[7]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 800:
                            Console.WriteLine("Hvilket spil afbrydes af 'Leeeeeeroy Jenkiiiins'\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[8]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[8])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[8]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 1000:
                            Console.WriteLine("Indie-spil hvor dine tårer er dit våben\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[9]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[9])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[9]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[10]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[10])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[10]}");
                                    break;
                                }
                            }
                            break;
                    }
                }
                #endregion
                #region film
                if (spillerKategoriValg == "film")
                {
                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                    switch (spillerVærdiValg) //spil
                    {
                        case 200:
                            Console.WriteLine("'I'll be back'\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[11]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[11])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[11]}");
                                    break;
                                }
                            }
                            break;
                        case 400:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[12]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[12])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[12]}");
                                    break;
                                }
                            }
                            break;
                        case 600:
                            Console.WriteLine("nå\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[13]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[13])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[13]}");
                                    break;
                                }
                            }
                            break;
                        case 800:
                            Console.WriteLine("hej\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[14]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[14])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[14]}");
                                    break;
                                }
                            }
                            break;
                        case 1000:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[15]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[15])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[15]}");
                                    break;
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[16]))
                            {
                                //læg point sammen
                                spillerPoint[0, 0] = +spillerVærdiValg;
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[16])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[16]}");
                                    break;
                                }
                            }
                            break;
                    }
                }
                #endregion
                #region natur
                if (spillerKategoriValg == "natur")
                {

                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                    switch (spillerVærdiValg) //spil
                    {
                        case 200:
                            Console.WriteLine("Den kan si' miav og spinde\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[17]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[17])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[17]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 400:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[18]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[18])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[18]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 600:
                            Console.WriteLine("nå\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[19]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[19])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[19]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 800:
                            Console.WriteLine("hej\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[20]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[20])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[20]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 1000:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[21]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[21])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[21]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            break;
                    }
                }
                #endregion
                #region quotes
                if (spillerKategoriValg == "quotes")
                {
                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                    switch (spillerVærdiValg) //spil
                    {
                        case 200:
                            Console.WriteLine("Den kan si' miav og spinde\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[22]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[22])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[22]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 400:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[23]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[23])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[23]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 600:
                            Console.WriteLine("nå\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[24]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[24])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[24]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 800:
                            Console.WriteLine("hej\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[25]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[25])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[25]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 1000:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[26]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[26])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[26]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            break;
                    }
                }
                #endregion
                #region dumt
                if (spillerKategoriValg == "dumt")
                {
                    Console.WriteLine("Til hvilken værdi?");
                    int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                    switch (spillerVærdiValg) //spil
                    {
                        case 200:
                            Console.WriteLine("Den kan si' miav og spinde\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[27]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[27])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[27]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 400:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[28]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[28])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[28]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 600:
                            Console.WriteLine("nå\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[29]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[29])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[29]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 800:
                            Console.WriteLine("hej\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[30]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[30])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[30]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        case 1000:
                            Console.WriteLine("sgfhdfsg\n - Hvad/hvem er ..:");
                            spillerSvar = Console.ReadLine().ToLower();
                            if (spillerSvar.Contains(rigtigSvar[31]))
                            {
                                spillerPoint[0, 0] = +spillerVærdiValg; //læg point sammen
                                Console.WriteLine($"Korrekt! Du har nu: {spillerPoint[0, 0]} point");
                                Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                break;
                            }
                            else
                            {
                                while (spillerSvar != rigtigSvar[31])
                                {
                                    Console.WriteLine("Forkert svar");
                                    Console.WriteLine($"Det rigtige svar er {spillerSvar = rigtigSvar[31]}");
                                    Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                                    break;
                                }
                            }

                            break;
                        default:
                            Console.WriteLine("Dur ikke, prøv igen.");
                            break;
                    }
                }
                #endregion

                while (spillerTur == 1)
                {
                    spillerTur++;
                }
            }

            Console.ReadKey();
            #endregion
        }
    }
}