using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
