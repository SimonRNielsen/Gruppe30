using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamecollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Oprettelse af array brættet
            string[,] mitBræt = new string[11, 11];
            for (int x = 0; x < mitBræt.GetLength(0); x++)
            {
                for (int y = 0; y < mitBræt.GetLength(1); y++)
                {
                    mitBræt[x,y]= "_ ";
                }
            }
            //Oprettelse af placering af bordet
            string[] mitVertical = new string[] { "  ", "A ", "B ", "C ", "D ", "E ", "F ", "G ", "H ", "I ", "J " };
            string[] mitHorisontal = new string[] { "  ", "1  ", "2  ", "3  ", "4  ", "5  ", "6  ", "7  ", "8  ", "9  ", "10 " };
            for ( int i =0; i < mitBræt.GetLength(1); i++)
            {
                mitBræt[0, i] = mitVertical[i];
                mitBræt[i, 0] = mitHorisontal[i];
            }

            //Oprettelse af de forskellige både
            string[] båd2 = new string[] { "🚢 ", "🚢 " }; // 4 både af 2 længde
            string[] båd3 = new string[] { "🚢 ", "🚢 ", "🚢 " }; // 3 både af 3 længde
            string[] båd4 = new string[] { "🚢 ", "🚢 ", "🚢 ", "🚢 " }; // 2 både af 4 længde
            string[] båd5 = new string[] { "🚢 ", "🚢 ", "🚢 ", "🚢 ", "🚢 " }; // 1 både af 5 længde


            //Placering af båd
            Random random = new Random(); //Random placering af båden
                                          //Båd 2
            for ( int j =0; j <5; j++)
            {
                int randomTing = random.Next(0, 2);

                if (randomTing == 0)
                {
                    int randomNumber1 = random.Next(1, 9);
                    int randomNumber2 = random.Next(1, 10);
                    for (int i = 0; i < båd2.Length; i++)
                    {
                        mitBræt[randomNumber1 +i, randomNumber2] = båd2[i];
                    }
                }
                else
                {
                    int randomNumber1 = random.Next(1, 10);
                    int randomNumber2 = random.Next(1, 9);
                    for (int i = 0; i < båd2.Length; i++)
                    {
                        mitBræt[randomNumber1, randomNumber2 + i] = båd2[i];
                    }
                }
            }
             

            //Båd 3 
            /*
            for ( int j =0; j < 3; j++)
            {
                int randomNumber1 = random.Next(1, 9);
                int randomNumber2 = random.Next(1, 9);
                int randomTing = random.Next(0, 2);

                if (randomTing == 0)
                {
                    for (int i = 0; i < båd3.Length; i++)
                    {
                        mitBræt[randomNumber1, randomNumber2 + i] = båd3[i];
                    }
                }
                else
                {
                    for (int i = 0; i < båd3.Length; i++)
                    {
                        mitBræt[randomNumber1 + i, randomNumber2] = båd3[i];
                    }
                }
            } */


            //Båd 5
            /*
            for (int j = 0; j < 1; j++)
            {
                int randomNumber1 = random.Next(1,4);
                int randomNumber2 = random.Next(1,4);
                int randomTing = random.Next(0, 2);

                if (randomTing == 0)
                {
                    for (int i = 0; i < båd5.Length; i++)
                    {
                        mitBræt[randomNumber1, randomNumber2 + i] = båd5[i];
                    }
                }
                else
                {
                    for (int i = 0; i < båd5.Length; i++)
                    {
                        mitBræt[randomNumber1 + i, randomNumber2] = båd3[i];
                    }
                }
            }
            */

            //Printe brættet
            for (int x =0; x < mitBræt.GetLength(0); x++)
            {
                for (int y =0; y <mitBræt.GetLength(1); y++)
                {
                    Console.Write(mitBræt[x, y]);
                }
                Console.WriteLine(" ");
            }



            Console.ReadKey();
        }

       
    }
}
