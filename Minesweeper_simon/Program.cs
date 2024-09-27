using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_simon
{
    internal class Program
    {
        enum board_ui : int
        {
            X = 9,
            O = 0
        }
        
        static void Main(string[] args)
        {
            int board_x_length = 0;
            int board_y_length = 0;
            int bomb_amount = 0;
            string input;
            string difficulty;
            do
            {
                Console.WriteLine("Hvor langt skal dit bræt være? (tal - f.eks. 15, minimum 10)");
                input = Console.ReadLine();
                int.TryParse(input, out board_x_length);
            } while (board_x_length == 0);
            if (board_x_length < 10)
            {
                board_x_length = 10;
            }
            do
            {
                Console.WriteLine("Hvor bredt skal dit bræt være? (tal - f.eks 30, minimum 10)");
                input = Console.ReadLine();
                int.TryParse(input, out board_y_length);
            } while (board_y_length == 0);
            if (board_y_length < 10)
            {
                board_y_length = 10;
            }
            do
            {
                Console.WriteLine("Hvor svært skal det være? (skriv let, medium, svær, FUCK");
                difficulty = Console.ReadLine().ToLower();
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
                    case "fuck":
                        bomb_amount = ((board_x_length * board_y_length) / 10) * 5;
                        break;
                }
            } while (bomb_amount == 0);
            Console.Clear();
            int[,] board = new int[board_x_length, board_y_length];
            int[,] playerboard = new int[board_x_length, board_y_length];
            Random bombplacer = new Random();
            int bomb_x;
            int bomb_y;
            int adjacent_bomb;

            for (int i = 0; i < bomb_amount; i++)
            {
                bomb_x = bombplacer.Next(0, board.GetLength(0));
                bomb_y = bombplacer.Next(0, board.GetLength(1));
                if (board[bomb_x, bomb_y] == 9)
                {
                    i--;
                }
                else
                {
                    board[bomb_x, bomb_y] = 9;
                }
            }
            for (int a_x = 0; a_x < board.GetLength(0); a_x++)
            {
                for (int a_y = 0; a_y < board.GetLength(1); a_y++)
                {
                    if (board[a_x,a_y] == 0)
                    {
                        adjacent_bomb = 0;
                        if (== 9) 
                        {
                            adjacent_bomb++;
                        }
                    }
                }
            }

            for (int b_x = 0; b_x < board_x_length; b_x++)
            {
                for (int b_y = 0; b_y < board_y_length; b_y++)
                {
                    Console.Write((board_ui)board[b_x, b_y] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
