using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Rikke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Oprettelse af array brættet
            Random random = new Random(); //Random variabel som oprettes fra starten, da der er flere metoder, som bruger Random. 
            int arraySize = ArraySize();
            string[,] spillerBraet = new string[arraySize, arraySize]; //Spillerens bræt til at skyde skibe ned med
            TegnBraet(spillerBraet);
            string[,] spillerBraetSkib = new string[arraySize, arraySize]; //Spillerens bræt med skibe
            TegnBraet(spillerBraetSkib);

            string[,] pcBraet = new string[arraySize, arraySize]; //Computerens bræt til at skyde skibe ned med
            TegnBraet(pcBraet);
            string[,] pcBraetSkib = new string[arraySize, arraySize]; //Computerens bræt
            TegnBraet(pcBraetSkib);

            //Variabler       
            string skib = "■ "; //Skib
            string forbi = "X "; //Forbier
            int spillerScore = 0; //Antal ramte både - spilleren
            int pcScore = 0; //Antal ramte både - computeren
            bool spilstarter = true;

            //Regler
            Console.WriteLine("Indtast dit navn");
            string spillerNavn = Console.ReadLine();
            Console.WriteLine($"Velkommen {spillerNavn} til sænke slagsskibe. Din modstander er computeren.");
            //Reglerne for sænke slagskib
            Console.WriteLine("Når spillet starter, skal du placere dine skibe på pladen. Du skal placere 5 skibe: hangarskibe (5), slagskibe (4), destroyer (3), ubåd (3) og patruljebåd (2). " +
                "De har en samlet længde på 17. Du placerer dem ved først at vælge retning og derefter koordinatet (0 el. 1), hvor du først bliver spurgt ind til bogstav og derefter tal. " +
                "Computeren gør det samme. Hvis du indtaster en orientering/koordinat, hvor skibet ikke kan placeres, bliver det placeret tilfældigt på pladen. ");
            Console.WriteLine($"Når alle skibene er placeret, starter spillet. Dig som spiller starter og derefter går det på tur. Når du skal forsøge at ramme et skib, skal du indtaste et " +
                $"koordinat på samme måde som placering af skibe. Hvis du rammer et skib, vil der frem så et {skib}, og en forbier bliver markeret med {forbi}. Efter hver runde bliver der vist " +
                $"scoren for både spilleren og computeren. Hvis du rammer et koordinat, hvor du tidligere har ramt, vil dit skud blive tilfældigt placeret. ");
            Console.WriteLine("Den som først når til 17 vinder (samlet antal skibs koordinat) og derefter slutter spillet. Hvis du ønsker at stoppe spillet og vende tilbage til menu’en, " +
                "skal du skrive “exit”. ");
            Console.ReadKey();

            /////////////////// MANGLER EXIT MULIGHED ////////////////



            while (spilstarter)
            {
                // TEGNE SKIBE
                PrintBraet(spillerBraet);
                //Spilleren placer skibene
                SpillerTegnSkibe(5, spillerBraetSkib, random, out spilstarter); 
                if (spilstarter == false) { break; }
                SpillerTegnSkibe(4, spillerBraetSkib, random, out spilstarter);
                if (spilstarter == false) { break; }
                SpillerTegnSkibe(3, spillerBraetSkib, random, out spilstarter);
                if (spilstarter == false) { break; }
                SpillerTegnSkibe(3, spillerBraetSkib, random, out spilstarter);
                if (spilstarter == false) { break; }
                SpillerTegnSkibe(2, spillerBraetSkib, random, out spilstarter);
                if (spilstarter == false) { break; }

                //COMPUTERENS SKIBE
                TegnSkibe(5, pcBraetSkib, random); //Hangarskib
                TegnSkibe(4, pcBraetSkib, random); //Slagskib
                TegnSkibe(3, pcBraetSkib, random); //Destroyer
                TegnSkibe(3, pcBraetSkib, random); //Ubåd
                TegnSkibe(2, pcBraetSkib, random); //Patrujlebåd

                //Clear console vinduet for at indikere, at man går til en ny fase i spillet
                Console.Clear();

                Console.WriteLine("Spillerens skibe");
                PrintBraet(spillerBraetSkib);
                Console.WriteLine("Spillerens plade");
                PrintBraet(spillerBraet);

                bool spil = true;
                while (spil)
                {
                    spillerScore = SkydSkib(pcBraetSkib, spillerBraet, spillerScore, random, out spilstarter);
                    if (spilstarter == false) { spil = false; }

                    pcScore = PCSkydSkib(spillerBraetSkib, pcBraet, pcScore, random);

                    Console.WriteLine("Spillerens skibe");
                    PrintBraet(spillerBraetSkib);
                    Console.WriteLine("Spillerens plade");
                    PrintBraet(spillerBraet);
                    Console.WriteLine($"Spillerens score: {spillerScore}");
                    Console.WriteLine($"Computerens score: {pcScore}");

                    //Når en har vundet spillet, vil der komme en besked frem til spilleren om udfaldet 
                    if (spillerScore == 17)
                    {
                        Console.WriteLine($"Tillykke {spillerNavn}, du vandt");
                        spil = false;
                        spilstarter = false;
                    }
                    if (pcScore == 17)
                    {
                        Console.WriteLine($"Desvære {spillerNavn}, du tabte");
                        spil = false;
                        spilstarter = false;
                    }
                }
            }

            Console.ReadKey();
        }

        static int ArraySize()
        {
            return 11;
        }

        static int TalOrientering()
        {
            return 1;
        }

        static int BogstavOrientering()
        {
            return 2;
        }

        static int RandomOrientering(Random random)
        {
            return random.Next(1, 3);
        }

        /// <summary>
        /// Spilleren placere selv sine egne skibe
        /// </summary>
        /// <param name="skibslængde">Skibets længde</param>
        /// <param name="spillerBraetSkibe">Array brættet</param>
        /// <param name="random">Random variabel som er oprettet fra starten</param>
        static void SpillerTegnSkibe(int skibslængde, string[,] spillerBraetSkibe, Random random, out bool spilstarter)
        {
            spilstarter = true;
            int arraySize = ArraySize();
            bool spil = true;

            while (spil)
            {
                //I tilfælde af, at spilleren indtaster et invalid input
                // Konvertering af inputOrientering 
                Console.WriteLine("Hvor vil du sætte dit skib?");
                Console.WriteLine("Hvilken orientering?  1 for vertikal ↓ 2 for for horisontal →");
                string inputOrientering = Console.ReadLine().ToLower();
                if (inputOrientering == "exit")
                {
                    spilstarter = false;
                    spil = false;
                    break;
                }

                //Hvis orientering ligger udenfor intervalet, bliver der generet en tilfældig orientering 
                int orientering = InvalidInput(inputOrientering, TalOrientering(), BogstavOrientering() + 1, random);
                if (orientering < 1 || orientering > 2)
                {
                    Console.WriteLine("Dit input er invalid. Der blive generet en tilfældig orientering.");
                    int randomOrientering = RandomOrientering(random);
                    orientering = randomOrientering;
                }

                //Indtastning af koordinat
                Console.WriteLine("Hvilket bogstav:");
                string inputBogstav = Console.ReadLine().ToLower();
                if (inputBogstav == "exit") { spil = false;  spilstarter = false; break; }

                Console.WriteLine("Hvilket tal:");
                string inputPlaceringTal = Console.ReadLine().ToLower();
                if (inputPlaceringTal == "exit") { spil = false;  spilstarter = false; break; }
                int placeringTal = InvalidInput(inputPlaceringTal, 1, arraySize, random);


                //Switch til at konvertere bogstav inputtet om til et koordinat
                int placeringBogstav;
                int randomBogstav = random.Next(1, arraySize);
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
                
                bool placering = TjekPlacering(orientering, skibslængde, placeringTal, placeringBogstav, spillerBraetSkibe);
                
                string skib = "■ ";
                int talOrientering = TalOrientering();
                int bogstavOrientering = BogstavOrientering();

                if (placering)
                {
                    if (orientering == talOrientering)
                    {
                        for (int i = 0; i < skibslængde; i++)
                        {
                            spillerBraetSkibe[placeringTal + i, placeringBogstav] = skib;
                        }
                    }
                    if (orientering == bogstavOrientering)
                    {
                        for (int i = 0; i < skibslængde; i++)
                        {
                            spillerBraetSkibe[placeringTal, placeringBogstav + i] = skib;
                        }
                    }
                }
                else
                {
                    TegnSkibe(skibslængde, spillerBraetSkibe, random);
                }
                Console.Clear();

                PrintBraet(spillerBraetSkibe);
                spilstarter = true;
                spil = false;
            }
        }

    

        /// <summary>
        /// Hvis input fra Console.ReadLine() skal konverteres til int men ikke kan, så vil der blive genereret en tilfældig int inden for ønsket interval
        /// </summary>
        /// <param name="input">Indtastet input fra spilleren</param>
        /// <param name="inputMin">Min værdig for intervallet</param>
        /// <param name="inputMax">Max værdig for intervallet</param>
        /// <param name="random">Random variabel som er oprettet fra starten</param>
        /// <returns></returns>
        static int InvalidInput (string input, int inputMin, int inputMax, Random random)
        {
            // Hvis spilleren indtaster et input, som ikke kan konverteres til en int. Så skal den genere et random input inden for det oprigtige interval 
            int randomInput = random.Next(inputMin, inputMax);
            
            if (input == "0")
            {
                return randomInput;
            }
            if (int.TryParse(input, out int output)) // Hvis input kan konvertere til en int
            {
                return Convert.ToInt32(input);
            }
            else if (input == "")
            {
                return randomInput;
            }
            else //Komvertere input til en int uagtet om det er inden for intervalet 
            {
                Console.WriteLine("Dit input kan ikke konverteres til en int, derfor bliver der generet en ramdom int inden for intervalet. ");
                return randomInput;
            }

            
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
        /// <param name="random">Random variabel som er oprettet fra starten</param>
        static void TegnSkibe(int skibsLaengde, string[,] braet, Random random)
        {
            // Variabler
            string skib = "■ ";
            bool tegnSkibe = true;
            int arraySize = ArraySize();
            int talOrientering = TalOrientering();


            //Genere et skib et tilfældigt sted. Det gør den ved at vælge to tilfældige koordinater. Hvis skibet ikke kollidere med et andet skib eller er uden for brættet, vil Tjekplacering 
            //returnere true. Hvis den returnere false, vil der blive generet nye tilfældige kooordinater. Når den har tegnet skibet, vil tegnSkibe ændres til false og stoppet loopet 
            while (tegnSkibe)
            {
                int randomOritering = RandomOrientering(random);
                int tal = random.Next(1, arraySize);
                int bogstaver = random.Next(1, arraySize);

                bool placering = TjekPlacering(randomOritering, skibsLaengde, tal, bogstaver, braet); //Tjekker om skibet kan placeres

                if (!placering) //Genere nye placeringer
                {
                    continue;
                }

                if (randomOritering == talOrientering)
                {
                    for (int i = 0; i < skibsLaengde; i++)
                    {
                        braet[tal + i, bogstaver] = skib;
                        if (i == skibsLaengde - 1) // Afslutter loopet
                        {
                            tegnSkibe = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < skibsLaengde; i++) 
                    {
                        braet[tal, bogstaver + i] = skib;
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
        static bool TjekPlacering (int orientering, int skibslaengde, int tal, int bogstaver, string[,] braet)
        {
            string skib = "■ ";
            int arraySize = ArraySize();
            int talOrientering = TalOrientering();
            int bogstavOrientering = BogstavOrientering();

            //Tjekker for om et skib vil collidere med et andet skib eller er uden for brættet. Den returnere false, hvis skibet ikke kan oprettes. 
            if (orientering == talOrientering)
            {
                for (int j = 0; j < skibslaengde; j++)
                {
                    if (tal + j >= arraySize) //Den vil være uden for brættet
                    {
                        return false;
                    }
                    if (braet[tal + j, bogstaver].Contains(skib))
                    {
                        return false;
                    }

                }
            }
            else if (orientering == bogstavOrientering)
            {
                for (int i = 0; i < skibslaengde; i++) 
                {
                    if (bogstaver + i >= arraySize) //Den vil være uden for brættet
                    {
                        return false;
                    }
                    if (braet[tal, bogstaver + i].Contains(skib))
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
        /// <param name="random">Random variabel som er oprettet fra starten</param>
        /// <returns>Spillerens score</returns>
        
        
        static int SkydSkib (string[,] pcBraetSkibe, string[,] spillerBraet, int spillerScore, Random random, out bool spilstarter)
        {
            spilstarter = true;
            bool spil = true;
            int arraySize = ArraySize();
            string skib = "■ "; //Skib
            string forbi = "X "; //Forbier

            while (spil)
            {
                Console.WriteLine("Hvilket koordinat vil du ramme?");

                Console.WriteLine("Hvilket bogstav:");

                string inputBogstav = Console.ReadLine().ToLower();
                if (inputBogstav == "exit")
                {
                    spilstarter = false;
                    spil = false;
                    break;
                }
                //Switch til at konvertere bogstav inputtet om til et koordinat
                int angrebBogstav;
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
                    default:
                        Console.WriteLine("Du indtastede et indvalid svar, derfor er der valgt et tilfældigt bogstav.");
                        angrebBogstav = random.Next(1, arraySize); break;
                }

                Console.WriteLine("Hvilket tal:");
                string inputAngrebTal = Console.ReadLine();
                if (inputAngrebTal == "exit")
                {
                    spilstarter = false;
                    spil = false;
                    break;
                }

                int angrebTal = InvalidInput(inputAngrebTal, 1, arraySize, random);
                //Hvis spilleren indtaster et tal, som kan konvertes men er udenfor intervallet
                if (angrebTal < 1 || angrebTal > 10)
                {
                    Console.WriteLine("Du har indtastet en int, som er udenfor intervallet. Der er generet en tilfældig int");
                    angrebTal = random.Next(1, arraySize);
                }
                Console.Clear();

                //Tjekker om den har ramt et skib
                if (pcBraetSkibe[angrebTal, angrebBogstav].Contains(skib) && spillerBraet[angrebTal, angrebBogstav].Contains("_ "))
                {
                    Console.WriteLine("Du ramte et skib");
                    spillerBraet[angrebTal, angrebBogstav] = skib;
                    spillerScore++;
                    return spillerScore;
                }
                else if (pcBraetSkibe[angrebTal, angrebBogstav].Contains(forbi) && spillerBraet[angrebTal, angrebBogstav].Contains("_ "))
                {
                    Console.WriteLine("Du har allerede ramt koordinatet og er en forbier");
                    return spillerScore;
                }
                else
                {
                    Console.WriteLine("Forbier");
                    spillerBraet[angrebTal, angrebBogstav] = forbi;
                    return spillerScore;
                }
            }
            return spillerScore;
            
        } 

        /// <summary>
        /// Printer brættet
        /// </summary>
        /// <param name="braet">Array bræt</param>
        static void PrintBraet(string[,] braet)
        {
            int arraySize = ArraySize();
            for (int tal = 0; tal < arraySize; tal++)
            {
                for (int bogstav = 0; bogstav < arraySize; bogstav++)
                {
                    Console.Write(braet[tal, bogstav]);
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
        /// <param name="random">Random variabel som er oprettet fra starten</param>
        /// <returns>Returnere PC'ens score</returns>
        static int PCSkydSkib(string[,] spillerBraetSkibe, string[,] pcBraet, int pcScore, Random random)
        {
            //Variabler        
            string skib = "■ "; //Skib
            string forbi = "X "; //Forbier
            bool pcSkyde = true;
            int arraySize = ArraySize();
            while (pcSkyde)
            {
                //Skyder et random sted i arrayet
                int tal = random.Next(1, arraySize);
                int bogstaver = random.Next(1, arraySize);

                //Tjekker om den har ramt et skib

                if (spillerBraetSkibe[tal, bogstaver].Contains(skib) && pcBraet[tal, bogstaver].Contains("_ "))
                {
                    Console.WriteLine("PCen ramte dit skib");
                    pcBraet[tal, bogstaver] = skib;
                    pcScore++;
                    pcSkyde = false;
                    return pcScore;
                }
                else if(pcBraet[tal, bogstaver].Contains(forbi))
                {
                    continue;
                }
                else 
                {
                    Console.WriteLine("PCen ramte forbi");
                    pcBraet[tal, bogstaver] = forbi;
                    pcSkyde = false;
                    return pcScore;
                }

            }
            Console.Clear();
            return 100;
        }

    }

}