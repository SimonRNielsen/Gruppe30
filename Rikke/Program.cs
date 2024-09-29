using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rikke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Oprettelse af array brættet
            string[,] mitBraet = new string[11, 11]; //Spillerens bræt
            TegnBraet(mitBraet);

            string[,] pcBraet = new string[11, 11]; //Computerens bræt
            TegnBraet(pcBraet);

            //Chars        
            string baad = "■ "; //Båd
            string forbi = "X "; //Forbier


            // Test af at tegne både ///////////
            TegnBaade(5, pcBraet); //Hangarskib
            TegnBaade(4, pcBraet); //Slagskib
            TegnBaade(3, pcBraet); //Destroyer
            TegnBaade(3, pcBraet); //Ubåd
            TegnBaade(2, pcBraet); //Patrujlebåd

            Console.WriteLine("PC bræt");
            PrintBraet(pcBraet);

            SkydBaad(pcBraet, mitBraet);


            //Printe brættet
            Console.WriteLine("Spillers bræt");
            PrintBraet(mitBraet);
            Console.WriteLine("PC bræt");
            PrintBraet(pcBraet);
            
            Console.ReadKey();
        }

        /// <summary>
        /// Udyldelse af et bræt på 10x10 med nummering af 1-10 og A-J
        /// </summary>
        /// <param name="braet">Array brættet</param>
        static void TegnBraet(string[,] braet)
        {
            for (int x = 0; x < braet.GetLength(0); x++)
            {
                for (int y = 0; y < braet.GetLength(1); y++)
                {
                    braet[x, y] = "_ ";
                }
            }
            //Oprettelse af placering af bordet
            string[] mitVertical = new string[] { "   ", " A ", "B ", "C ", "D ", "E ", "F ", "G ", "H ", "I ", "J " };
            string[] mitHorisontal = new string[] { "  ", "1  ", "2  ", "3  ", "4  ", "5  ", "6  ", "7  ", "8  ", "9  ", "10 " };
            for (int i = 0; i < braet.GetLength(1); i++)
            {
                braet[0, i] = mitVertical[i];
                braet[i, 0] = mitHorisontal[i];
            }
        }

        /// <summary>
        /// Placering af både
        /// </summary>
        /// <param name="antalBaade">Antal af både</param>
        /// <param name="baadlaengde">Bådens længde</param>
        /// <param name="mitBraet">Spillebræts arrayet</param>
        static void TegnBaade(int baadLaengde, string[,] braet)
        {
            string baad = "■ ";
            Random random = new Random();
            bool tegnBaade = true;

            while (tegnBaade)
            {
                int randomOritering = random.Next(0, 2);
                int randomNumber1 = random.Next(1, braet.GetLength(0) - baadLaengde);
                int randomNumber2 = random.Next(1, braet.GetLength(1) - 1);

                bool placering = TjekPlacering(randomOritering, baadLaengde, randomNumber1, randomNumber2, braet);

                if (placering == true)
                {
                    if (randomOritering == 0)
                    {
                        for (int i = 0; i < baadLaengde; i++)
                        {
                            braet[randomNumber1 + i, randomNumber2] = baad;
                            if (i == baadLaengde-1)
                            {
                                tegnBaade = false;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < baadLaengde; i++)
                        {
                            braet[randomNumber2, randomNumber1 + i] = baad;
                            if (i == baadLaengde - 1)
                            {
                                tegnBaade = false;
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Tjekker for at bådene ikke collidere med hinanden
        /// </summary>
        /// <param name="orientering"></param>
        /// <param name="baadLaengde"></param>
        /// <param name="randomNumber1"></param>
        /// <param name="randomNumber2"></param>
        /// <param name="mitBraet"></param>
        /// <returns></returns>
        static bool TjekPlacering (int orientering, int baadLaengde, int randomNumber1, int randomNumber2, string[,] braet)
        {
            string baad = "■ ";
            if (orientering == 0)
            {
                for (int j = 0; j < baadLaengde; j++)
                {
                    if (braet[randomNumber1 + j, randomNumber2].Contains(baad))
                    {
                        continue;
                    }
                    return true;

                }
            }
            else
            {
                for (int i = 0; i < baadLaengde; i++)
                {
                    if (braet[randomNumber2, randomNumber1 + i].Contains(baad))
                    {
                        continue;
                    }
                    return true;          
                }
            }
            return false;
        }

        /// <summary>
        /// Angribe pcens skibe
        /// </summary>
        /// <param name="pcBraet">modstanderens bræt (pc)</param>
        /// <param name="mitBraet">eget bræt</param>
        static void SkydBaad (string[,] pcBraet, string[,] mitBraet)
        {
            //Chars        
            string baad = "■ "; //Båd
            string forbi = "X "; //Forbier

            //Vælg et koordinat at ramme
            Console.WriteLine("Hvilket koordinat vil du ramme?");
            Console.WriteLine("Hvilket bogstav:");
            string inputBogstav = Console.ReadLine().ToLower();
            Console.WriteLine("Hvilket tal:");
            int angrebTal = Convert.ToInt32(Console.ReadLine());

            // Konvetering af bogstav koordinat til en int
            int angrebBogstav = -1;
            switch (inputBogstav)
            {
                case ("a"): angrebBogstav = 1; break;
                case ("b"): angrebBogstav = 2; break;
                case ("c"): angrebBogstav = 3; break;
                case ("d"): angrebBogstav = 4; break;
                case ("e"): angrebBogstav = 5; break;
                case ("f"): angrebBogstav = 6; break;
                case ("g"): angrebBogstav = 7; break;
                case ("h"): angrebBogstav = 8; break;
                case ("i"): angrebBogstav = 9; break;
                case ("j"): angrebBogstav = 10; break;
            }

            Console.WriteLine(pcBraet[angrebTal, angrebBogstav]);

            //Tjekker om den har ramt et skib
            if (pcBraet[angrebTal, angrebBogstav].Contains(baad))
            {
                Console.WriteLine("Du ramte et skib");
                mitBraet[angrebTal, angrebBogstav] = baad;
            }
            else
            {
                Console.WriteLine("Forbier");
                mitBraet[angrebTal, angrebBogstav] = forbi;
            }
        }

        /// <summary>
        /// Printer brættet
        /// </summary>
        /// <param name="braet">Array bræt</param>
        static void PrintBraet(string[,] braet)
        {
            for (int x = 0; x < braet.GetLength(0); x++)
            {
                for (int y = 0; y < braet.GetLength(1); y++)
                {
                    Console.Write(braet[x, y]);
                }
                Console.WriteLine(" ");
            }
        }
    }
}