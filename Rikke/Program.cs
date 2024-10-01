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
            string skib = "■ "; //Båd
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
            Console.WriteLine($"Hvis du rammer et skibe, fik der på koordinattet fremgå {skib}, hvis det er en forbier fremgår der {forbi}.");
            Console.WriteLine("Der vil ydermere få oplyst, om du har ramt et skib eller forbi. Du vil også få oplyst, om computeren rammer.");
            Console.WriteLine("Efter hver runde får du oplyst scoren, hvor den som når først til 17 (den samlet længde skibe) vinder");
            Console.WriteLine("Held og lykke. Tryk på en tast for at starte.");
            Console.ReadKey();

            // TEGNE SKIBE
            /*
            TegnBaade(5, spillerBraetBaade); //Hangarskib
            TegnBaade(4, spillerBraetBaade); //Slagskib
            TegnBaade(3, spillerBraetBaade); //Destroyer
            TegnBaade(3, spillerBraetBaade); //Ubåd
            TegnBaade(2, spillerBraetBaade); //Patrujlebåd
            */
            //Spilleren placer skibene
            SpillerTegnBaade(5, spillerBraetBaade);
            SpillerTegnBaade(4, spillerBraetBaade);
            SpillerTegnBaade(3, spillerBraetBaade);
            SpillerTegnBaade(3, spillerBraetBaade);
            SpillerTegnBaade(2, spillerBraetBaade);


            //COMPUTERENS SKIBE
            TegnBaade(5, pcBraetBaade); //Hangarskib
            TegnBaade(4, pcBraetBaade); //Slagskib
            TegnBaade(3, pcBraetBaade); //Destroyer
            TegnBaade(3, pcBraetBaade); //Ubåd
            TegnBaade(2, pcBraetBaade); //Patrujlebåd

            //Clear console vinduet for at indikere, at man går til en ny fase i spillet
            Console.Clear();

            Console.WriteLine("Spillerens skibe");
            PrintBraet(spillerBraetBaade);
            Console.WriteLine("Spillerens plade");
            PrintBraet(spillerBraet);
            
            //For at vinde skal man have 17 point i alt, hvilket svare til den samlet længde på skibene. Spilleren starter
            while (spilstarter)
            {
                spillerScore = SkydBaad(pcBraetBaade, spillerBraet, spillerScore); 

                pcScore =PCSkydBaad(spillerBraetBaade, pcBraet, pcScore);

                Console.WriteLine("Spillerens skibe");
                PrintBraet(spillerBraetBaade);
                Console.WriteLine("Spillerens plade");
                PrintBraet(spillerBraet);
                Console.WriteLine($"Spillerens score: {spillerScore}");
                Console.WriteLine($"Computerens score: {pcScore}");
                
                if (spillerScore == 17)
                {
                    Console.WriteLine("Tillykke, du vandt");
                    spilstarter = false;
                }
                if (pcScore == 17)
                {
                    Console.WriteLine("Desvære, du tabte");
                    spilstarter = false;
                }
            }
            
            Console.ReadKey();
        }

        /// <summary>
        /// Spilleren placere selv sine egne skibe
        /// </summary>
        /// <param name="skibslængde">Skibets længde</param>
        /// <param name="spillerBraetBaade">Array brættet</param>
        static void SpillerTegnBaade (int skibslængde, string[,] spillerBraetBaade)
        {
            Console.WriteLine("Hvor vil du sætte dit skib?");
            Console.WriteLine("Hvilken orientering?  0 for vertikal ↓ 1 for for horisontal →");
            int orientering = Convert.ToInt32(Console.ReadLine());

            //Hvilke begrænsning har spilleren for at sætte skibet 
            Random random = new Random();
            int randomOrientering = random.Next(0, 2);

            if (orientering == 0)
            {
                switch (skibslængde)
                {
                    case (5): Console.WriteLine("Skibet kan ligge mellem A-F"); break;
                    case (4): Console.WriteLine("Skibet kan ligge mellem A-G"); break;
                    case (3): Console.WriteLine("Skibet kan ligge mellem A-H"); break;
                    case (2): Console.WriteLine("Skibet kan ligge mellem A-I"); break;
                }
            }
            else
            {
                switch (skibslængde)
                {
                    case (5): Console.WriteLine("Skibet kan ligge mellem 1-6"); break;
                    case (4): Console.WriteLine("Skibet kan ligge mellem 1-7"); break;
                    case (3): Console.WriteLine("Skibet kan ligge mellem 1-8"); break;
                    case (2): Console.WriteLine("Skibet kan ligge mellem 1-9"); break;
                }
            }
            if (orientering < 0 || orientering > 1)
            {
                Console.WriteLine("Dit input er invalid. Der blive generet en tilfældig orientering.");
                orientering = randomOrientering;
            }

            //Indtastning af koordinat
            Console.WriteLine("Hvilket bogstav:");
            string inputBogstav = Console.ReadLine().ToLower();
            Console.WriteLine("Hvilket tal:");
            int placeringTal = Convert.ToInt32(Console.ReadLine());

            //Hvis spilleren indtaster et invalid koordinat
            int randomBogstav = random.Next(1, 10);
            if (placeringTal < 1 || placeringTal > 10)
            {
                int randomTal = random.Next(1, 10);
                placeringTal = randomTal;
            }
            int placeringBogstav = -1;

            //Switch til at konvertere bogstav inputtet om til et koordinat
            switch (inputBogstav)
            {
                case ("a"): placeringBogstav = 1; break;
                case ("b"): placeringBogstav = 2; break;
                case ("c"): placeringBogstav = 3; break;
                case ("d"): placeringBogstav = 4; break;
                case ("e"): placeringBogstav = 5; break;
                case ("f"): placeringBogstav = 6; break;
                case ("g"): placeringBogstav = 7; break;
                case ("h"): placeringBogstav = 8; break;
                case ("i"): placeringBogstav = 9; break;
                case ("j"): placeringBogstav = 10; break;
                default:
                    Console.WriteLine("Du indtastede et indvalid svar, derfor er der valgt et tilfældigt bogstav.");
                    placeringBogstav = randomBogstav; break;
            }

            //Tjekker om skibet kolidere med et andet skib
            bool placering = TjekPlacering(orientering, skibslængde, placeringBogstav, placeringTal, spillerBraetBaade);
            string skib = "■ ";


            if (placering)
            {
                if (orientering == 0)
                {
                    for (int i = 0; i < skibslængde; i++)
                    {
                        spillerBraetBaade[placeringBogstav + i, placeringTal] = skib;
                    }
                }
                if (orientering ==1)
                {
                    for (int i = 0; i < skibslængde; i++)
                    {
                        spillerBraetBaade[placeringTal, placeringBogstav + i] = skib;
                    }
                }
            }
            else
            {
                TegnBaade(skibslængde, spillerBraetBaade);
            }

            PrintBraet(spillerBraetBaade);
        }


        /// <summary>
        /// Udyldelse af et bræt på 10x10 med nummering af 1-10 og A-J
        /// </summary>
        /// <param name="braet">Array brættet</param>
        static void TegnBraet(string[,] braet)
        {
            //Udfylder hele arrayet med "_ "
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
        /// <param name="skibsLaengde">Bådens længde</param>
        /// <param name="braet">Spillebræts arrayet</param>
        static void TegnBaade(int skibsLaengde, string[,] braet)
        {
            // Variabler
            string skib = "■ ";
            bool tegnSkibe = true;

            //Genere et skib et tilfældigt sted. Det gør den ved at vælge to tilfældige koordinater. Hvis skibet ikke kollidere med et andet skib eller er uden for brættet, vil Tjekplacering 
            //returnere true. Hvis den returnere false, vil der blive generet nye tilfældige kooordinater. Når den har tegnet skibet, vil tegnSkibe ændres til false og stoppet loopet 
            while (tegnSkibe)
            {
                Random random = new Random();
                int randomOritering = random.Next(0, 2);
                int skibPlacering1 = random.Next(1, braet.GetLength(0) - skibsLaengde);
                int skibPlacering2 = random.Next(1, braet.GetLength(1) - 1);

                bool placering = TjekPlacering(randomOritering, skibsLaengde, skibPlacering1, skibPlacering2, braet); //Tjekker om skibet kan placeres

                if (!placering) //Genere nye placeringer
                {
                    continue;
                }

                if (randomOritering == 0)
                {
                    for (int i = 0; i < skibsLaengde; i++)
                    {
                        braet[skibPlacering1 + i, skibPlacering2] = skib;
                        if (i == skibsLaengde-1) // Afslutter loopet
                        {
                            tegnSkibe = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < skibsLaengde; i++)
                    {
                        braet[skibPlacering2, skibPlacering1 + i] = skib ;
                        if (i == skibsLaengde - 1) // Afslutter loopet
                        {
                            tegnSkibe = false;
                        }
                    }
                }
                
            }

        }

        

        /// <summary>
        /// Tjekker for at bådene ikke collidere med hinanden
        /// </summary>
        /// <param name="orientering"></param>
        /// <param name="skibslaengde"></param>
        /// <param name="skibPlacering1"></param>
        /// <param name="skibPlacering2"></param>
        /// <param name="braet"></param>
        /// <returns></returns>
        static bool TjekPlacering (int orientering, int skibslaengde, int skibPlacering1, int skibPlacering2, string[,] braet)
        {
            string skib = "■ ";

            //Tjekker for om et skib vil collidere med et andet skib eller er uden for brættet. Den returnere false, hvis skibet ikke kan oprettes. 
            if (orientering == 0)
            {
                for (int j = 0; j < skibslaengde; j++)
                {
                    if (skibPlacering1 + j == 11) //Den vil være uden for brættet
                    {
                        return false;
                    }
                    if (braet[skibPlacering1 + j, skibPlacering2].Contains(skib))
                    {
                        return false;
                    }

                }
            }
            else
            {
                for (int i = 0; i < skibslaengde; i++) 
                {
                    if (skibPlacering1 + i == 11) //Den vil være uden for brættet
                    {
                        return false;
                    }
                    if (braet[skibPlacering2, skibPlacering1 + i].Contains(skib))
                    {
                        return false;
                    }
                }
            }
            //Kan oprette skibet 
            return true;
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
            string skib = "■ "; //Skib
            string forbi = "X "; //Forbier

            //Vælg et koordinat at ramme
            Console.WriteLine("Vær opmærsom på, at du kan vælge et koordinat, som du allerede har gættet. Hvis du gør det, får du ikke et nyt forsøg.");
            Console.WriteLine("Hvis du ved et uheld indtaster et indvalid svar, vil der blive valgt et tilfældigt koordinat.");
            Console.WriteLine("Hvilket koordinat vil du ramme?");
            Console.WriteLine("Hvilket bogstav:");
            string inputBogstav = Console.ReadLine().ToLower();
            Console.WriteLine("Hvilket tal:");
            int angrebTal = Convert.ToInt32(Console.ReadLine());

            //Hvis spilleren indtaster et invalid koordinat
            Random random = new Random();
            int randomBogstav = random.Next(1, 10);
            if (angrebTal<1 || angrebTal>10)
            {
                int randomTal = random.Next(1, 10);
                angrebTal = randomTal;
            }
            int angrebBogstav = -1;
            Console.Clear();

            //Switch til at konvertere bogstav inputtet om til et koordinat
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
                default: Console.WriteLine("Du indtastede et indvalid svar, derfor er der valgt et tilfældigt bogstav.");
                    angrebBogstav = randomBogstav; break;
            }

            //Tjekker om den har ramt et skib
            if (pcBraetSkibe[angrebTal, angrebBogstav].Contains(skib))
            {
                Console.WriteLine("Du ramte et skib");
                spillerBraet[angrebTal, angrebBogstav] = skib;
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
            string skib = "■ "; //Skib
            string forbi = "X "; //Forbier
            bool pcSkyde = true;

            while (pcSkyde)
            {
                //Skyder et random sted i arrayet
                Random random = new Random();
                int skibPlacering1 = random.Next(1, 10);
                int skibPlacering2 = random.Next(1, 10);


                Console.WriteLine(spillerBraetSkibe[skibPlacering1, skibPlacering2]);

                //Tjekker om den har ramt et skib
                if (spillerBraetSkibe[skibPlacering1, skibPlacering2].Contains("_ "))
                {
                    Console.WriteLine("PCen ramte forbi");
                    pcBraet[skibPlacering1, skibPlacering2] = forbi;
                    pcSkyde = false;
                    return pcScore;
                }
                if (spillerBraetSkibe[skibPlacering1, skibPlacering2].Contains(skib))
                {
                    Console.WriteLine("PCen ramte dit skib");
                    pcBraet[skibPlacering1, skibPlacering2] = skib;
                    pcScore++;
                    pcSkyde = false;
                    return pcScore;
                }
            }
            return 100;
        }

    }
}