using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Rikke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Oprettelse af array brættet
            string[,] spillerBraet = new string[11, 11]; //Spillerens bræt til at skyde skibe ned med
            TegnBraet(spillerBraet);
            string[,] spillerBraetBaade = new string[11, 11]; //Spillerens bræt med skibe
            TegnBraet(spillerBraetBaade);

            string[,] pcBraet = new string[11, 11]; //Computerens bræt til at skyde skibe ned med
            TegnBraet(pcBraet);
            string[,] pcBraetBaade = new string[11, 11]; //Computerens bræt
            TegnBraet(pcBraetBaade);

            //Variabler       
            string baad = "■ "; //Båd
            string forbi = "X "; //Forbier
            int spillerScore = 0; //Antal ramte både
            int pcScore = 0; //Antal ramte både
            bool spilstarter = true;

            //Regler
            Console.WriteLine("Indtast dit navn");
            string spillerNavn = Console.ReadLine();
            Console.WriteLine($"Velkommen {spillerNavn} til sænke slagsskibe");
            Console.WriteLine("Når spillet starter bliver der automatisk tilfældigt placeret bådene for både spilleren og computeren.");
            Console.WriteLine("Der er i alt 5 forskellige skibe med følgende længde: hangarskib (5), slagskibe (4), destroyer (3), ubåd (3) og patrujlebåd (2).");
            Console.WriteLine("Derefter vil runden gå på tur, hvor du starter. Du skal oplyse et koordinat fra, hvor du vil skyde efter et skib.");
            Console.WriteLine($"Hvis du rammer et skibe, fik der på koordinattet fremgå {baad}, hvis det er en forbier fremgår der {forbi}.");
            Console.WriteLine("Der vil ydermere få oplyst, om du har ramt et skib eller forbi. Du vil også få oplyst, om computeren rammer.");
            Console.WriteLine("Efter hver runde får du oplyst scoren, hvor den som når først til 17 (den samlet længde skibe) vinder");
            Console.WriteLine("Held og lykke. Tryk på en tast for at starte.");
            Console.ReadKey();

            // TEGNE SKIBE
            // SPILLERENS SKIBE
            TegnBaade(5, spillerBraetBaade); //Hangarskib
            TegnBaade(4, spillerBraetBaade); //Slagskib
            TegnBaade(3, spillerBraetBaade); //Destroyer
            TegnBaade(3, spillerBraetBaade); //Ubåd
            TegnBaade(2, spillerBraetBaade); //Patrujlebåd

            //COMPUTERENS SKIBE
            TegnBaade(5, pcBraetBaade); //Hangarskib
            TegnBaade(4, pcBraetBaade); //Slagskib
            TegnBaade(3, pcBraetBaade); //Destroyer
            TegnBaade(3, pcBraetBaade); //Ubåd
            TegnBaade(2, pcBraetBaade); //Patrujlebåd


            Console.WriteLine("PCen skibe");
            PrintBraet(pcBraetBaade);
            Console.WriteLine("Spillerens skibe");
            PrintBraet(spillerBraetBaade);

            while (spilstarter)
            {
                spillerScore = SkydBaad(pcBraetBaade, spillerBraet, spillerScore);

                pcScore =PCSkydBaad(spillerBraetBaade, pcBraet, pcScore);

                Console.WriteLine($"Spillerens score: {spillerScore}");
                Console.WriteLine($"Computerens score: {pcScore}");
                spilstarter = false;
            }

            //Printe brættet
            Console.WriteLine("Spillerens bræt");
            PrintBraet(spillerBraet);
            Console.WriteLine("Spillerens skibe");
            PrintBraet(spillerBraetBaade);
            
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
        /// <param name="pcBraetSkibe">modstanderens bræt (pc)</param>
        /// <param name="spillerBraet">eget bræt</param>
        /// <param name="spillerScore">Spillers score</param>
        /// <returns>Spillerens score</returns>
        static int SkydBaad (string[,] pcBraetSkibe, string[,] spillerBraet, int spillerScore)
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
            Console.Clear();

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

            Console.WriteLine(pcBraetSkibe[angrebTal, angrebBogstav]);

            //Tjekker om den har ramt et skib
            if (pcBraetSkibe[angrebTal, angrebBogstav].Contains(baad))
            {
                Console.WriteLine("Du ramte et skib");
                spillerBraet[angrebTal, angrebBogstav] = baad;
                spillerScore++;
                return spillerScore;
            }
            else
            {
                Console.WriteLine("Forbier");
                spillerBraet[angrebTal, angrebBogstav] = forbi;
                return spillerScore;
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

        /// <summary>
        /// PCen skyder
        /// </summary>
        /// <param name="spillerBraetSkibe">Spillerens bræt</param>
        /// <param name="pcBraet">PCen bræt</param>
        /// <param name="pcScore"> PC score</param>
        /// <returns>Computerens score</returns>
        static int PCSkydBaad(string[,] spillerBraetSkibe, string[,] pcBraet, int pcScore)
        {
            //Variabler        
            string baad = "■ "; //Båd
            string forbi = "X "; //Forbier
            bool pcSkyde = true;
            Random random = new Random();

            while (pcSkyde)
            {
                //Skyder et random sted i arrayet
                int randomNumber1 = random.Next(1, 10);
                int randomNumber2 = random.Next(1, 10);


                Console.WriteLine(spillerBraetSkibe[randomNumber1, randomNumber2]);

                //Tjekker om den har ramt et skib
                if (!spillerBraetSkibe[randomNumber1, randomNumber2].Contains(baad) ||!spillerBraetSkibe[randomNumber1, randomNumber2].Contains(forbi))
                {
                    Console.WriteLine("PCen ramte forbi");
                    pcBraet[randomNumber1, randomNumber2] = forbi;
                    pcSkyde = false;
                    return pcScore;
                }
                if (spillerBraetSkibe[randomNumber1, randomNumber2].Contains(baad))
                {
                    Console.WriteLine("PCen ramte dit skib");
                    pcBraet[randomNumber1, randomNumber2] = baad;
                    pcScore++;
                    pcSkyde = false;
                    return pcScore;
                }
            }
            return 100;
        }

    }
}