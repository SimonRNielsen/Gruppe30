using System;
using System.Collections.Generic;
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
            string baad = "■ ";


            // Test af at tegne både ///////////
            TegnBaade(2, 4, mitBraet);

            




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
        static void TegnBaade(int antalBaade, int baadlaengde, string[,] mitBraet)
        {
            string baad = "■ ";

            Random random = new Random();
            for (int j = 0; j < antalBaade; j++)
            {
                int randomOritering = random.Next(0, 2);
                int randomNumber1 = random.Next(1, mitBraet.GetLength(0)-1);
                int randomNumber2 = random.Next(1, mitBraet.GetLength(1)-1);
                if (randomOritering == 0)
                {
                    for (int i = 0; i < baadlaengde; i++)
                    {
                        mitBraet[randomNumber1 + i, randomNumber2] = baad;
                    }
                }
                else
                {
                    for (int i = 0; i < baadlaengde; i++)
                    {
                        mitBraet[randomNumber1, randomNumber2 + i] = baad; ;
                    }
                }
            }

        }
    }
}
