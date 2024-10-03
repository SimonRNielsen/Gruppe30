using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Jeopardy-blå
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();


            //string[] rigtigSvar = new string[] { "Kat", "Mario" };
            string rigtigSvar = "kat";


            #region Intro - spiller navneArray
            // Intro
            Console.WriteLine("Velkommen til Jeopardy! Det er spillet hvor du stiller spørgsmålene og vi har svarene!");

            Console.WriteLine("Der kan være mellem 1-3 spillere. Hvor mange spillere er I?");
            int antalSpillere = Convert.ToInt32(Console.ReadLine()); //////////////////// bool antalSpillere == 0<x || x<4 ?? så bed om x antal spilleres navne

            int[,] spillerPoint = new int [3,3];

            string spillerEt;
            string spillerTo;
            string spillerTre;

            for (int i = 0; i < antalSpillere; i++)
            {///////////////////////////////////////////////////////////// start spillet forfra ///////////////////////////////////////////////////////////////////////////////
                if (antalSpillere > 3)
                {
                    Console.WriteLine("Der kan max være 3 spillere. Start forfra.");
                    //Environment.Exit(ReadKey() == ConsoleKey.Enter;
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
            Console.Clear();


            Random random = new Random();
            int randomNumber = random.Next(1, 3);

            int aktivSpiller = randomNumber;


            #endregion

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
                Console.Write("\n \n");
            }
            #endregion



            //if (spillerSvar.Equals(rigtigSvar)) //ignore case, så man ikke får forkert svar på grund af casing
            //{
            //    StringComparison.OrdinalIgnoreCase;
            //}

            string spillerSvar = Console.ReadLine();
            Console.WriteLine($"Spiller {aktivSpiller} starter");

            //clear intro, gør plads til board
            Console.Clear();
            

            Console.WriteLine("Hvilken kategori vælger du?");
            string spillerKategoriValg = Console.ReadLine().ToLower();


            #region switch med spørgsmål

            //Her vælges spørgsmål, da det er en "opremsning" har jeg brugt en switch
            if (spillerKategoriValg == "dyr")
            {

                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());


                switch (spillerVærdiValg) //Dyr
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Hvad/hvem er ..:");
                        spillerSvar = Console.ReadLine().ToLower();
                        if (spillerSvar.Contains(rigtigSvar))
                        {
                            //læg point sammen
                            spillerPoint[0, 0] = + spillerVærdiValg;
                            Console.WriteLine($"Korrekt! Stillingen er nu: {spillerPoint[0,0]}");
                            Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Forkert svar");
                        }

                        break;
                    case 400:
                        Console.WriteLine("Muh\n - Hvad/hvem er ..:");
                        spillerSvar = Console.ReadLine().ToLower();
                        if (spillerSvar.Contains(rigtigSvar))
                        {
                            //læg point sammen
                            Console.WriteLine($"Korrekt! Stillingen er nu: PRINTscoreBoard");
                            Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                        }
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        spillerSvar = Console.ReadLine().ToLower();
                        if (spillerSvar.Contains(rigtigSvar))
                        {
                            //læg point sammen
                            Console.WriteLine($"Korrekt! Stillingen er nu: PRINTscoreBoard");
                            Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                        }
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        spillerSvar = Console.ReadLine().ToLower();
                        if (spillerSvar.Contains(rigtigSvar))
                        {
                            //læg point sammen
                            Console.WriteLine($"Korrekt! Stillingen er nu: PRINTscoreBoard");
                            Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                        }
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        spillerSvar = Console.ReadLine().ToLower();
                        if (spillerSvar.Contains(rigtigSvar))
                        {
                            //læg point sammen
                            Console.WriteLine($"Korrekt! Stillingen er nu: PRINTscoreBoard");
                            Console.WriteLine($"\nDet er spiller {aktivSpiller + 1}'s tur");
                        }
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }

            if (spillerKategoriValg == "spil")
            {

                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                switch (spillerVærdiValg) //spil
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 400:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }

            if (spillerKategoriValg == "film")
            {
                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                switch (spillerVærdiValg) //spil
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 400:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }

            if (spillerKategoriValg == "natur")
            {

                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                switch (spillerVærdiValg) //spil
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 400:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }

            if (spillerKategoriValg == "quotes")
            {
                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                switch (spillerVærdiValg) //spil
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 400:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }

            if (spillerKategoriValg == "dumt")
            {
                Console.WriteLine("Til hvilken værdi?");
                int spillerVærdiValg = Convert.ToInt32(Console.ReadLine());

                switch (spillerVærdiValg) //spil
                {
                    case 200:
                        Console.WriteLine("Den kan si' miav og spinde\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 400:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 600:
                        Console.WriteLine("nå\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 800:
                        Console.WriteLine("hej\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    case 1000:
                        Console.WriteLine("sgfhdfsg\n - Husk at svare med 'hvad/hvem er ..:");
                        break;
                    default:
                        Console.WriteLine("Dur ikke, prøv igen.");
                        break;
                }
            }
            Console.ReadKey();
            #endregion
        }
    }
}
