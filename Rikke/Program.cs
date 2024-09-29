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
            string[,] mitBraet = new string[11, 11];
            for (int x = 0; x < mitBraet.GetLength(0); x++)
            {
                for (int y = 0; y < mitBraet.GetLength(1); y++)
                {
                    mitBraet[x, y] = "_ ";
                }
            }
            //Oprettelse af placering af bordet
            string[] mitVertical = new string[] { "   ", "A ", "B ", "C ", "D ", "E ", "F ", "G ", "H ", "I ", "J " };
            string[] mitHorisontal = new string[] { "  ", "1  ", "2  ", "3  ", "4  ", "5  ", "6  ", "7  ", "8  ", "9  ", "10 " };
            for (int i = 0; i < mitBraet.GetLength(1); i++)
            {
                mitBraet[0, i] = mitVertical[i];
                mitBraet[i, 0] = mitHorisontal[i];
            }

            //Båd char         
            //string baad = "■ ";


            // Test af at tegne både ///////////
            TegnBaade(5, mitBraet); //Hangarskib
            TegnBaade(4, mitBraet); //Slagskib
            TegnBaade(3, mitBraet); //Destroyer
            TegnBaade(3, mitBraet); //Ubåd
            TegnBaade(2, mitBraet); //Patrujlebåd

            //Printe brættet
            for (int x = 0; x < mitBraet.GetLength(0); x++)
            {
                for (int y = 0; y < mitBraet.GetLength(1); y++)
                {
                    Console.Write(mitBraet[x, y]);
                }
                Console.WriteLine(" ");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Placering af både
        /// </summary>
        /// <param name="antalBaade">Antal af både</param>
        /// <param name="baadlaengde">Bådens længde</param>
        /// <param name="mitBraet">Spillebræts arrayet</param>
        static void TegnBaade(int baadLaengde, string[,] mitBraet)
        {
            string baad = "■ ";
            Random random = new Random();
            bool tegnBaade = true;

            while (tegnBaade)
            {
                int randomOritering = random.Next(0, 2);
                int randomNumber1 = random.Next(1, mitBraet.GetLength(0) - baadLaengde);
                int randomNumber2 = random.Next(1, mitBraet.GetLength(1) - 1);

                bool placering = TjekPlacering(randomOritering, baadLaengde, randomNumber1, randomNumber2, mitBraet);

                if (placering == true)
                {
                    if (randomOritering == 0)
                    {
                        for (int i = 0; i < baadLaengde; i++)
                        {
                            mitBraet[randomNumber1 + i, randomNumber2] = baad;
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
                            mitBraet[randomNumber2, randomNumber1 + i] = baad;
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
        /// 
        /// </summary>
        /// <param name="orientering"></param>
        /// <param name="baadLaengde"></param>
        /// <param name="randomNumber1"></param>
        /// <param name="randomNumber2"></param>
        /// <param name="mitBraet"></param>
        /// <returns></returns>
        static bool TjekPlacering (int orientering, int baadLaengde, int randomNumber1, int randomNumber2, string[,] mitBraet)
        {
            string baad = "■ ";
            if (orientering == 0)
            {
                for (int j = 0; j < baadLaengde; j++)
                {
                    if (mitBraet[randomNumber1 + j, randomNumber2].Contains(baad))
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
                    if (mitBraet[randomNumber2, randomNumber1 + i].Contains(baad))
                    {
                        continue;
                    }
                    return true;          
                }
            }
            return false;
        }
    }
}