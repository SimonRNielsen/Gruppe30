using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.DesignerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Philip_MasterMind
{
    internal class Program
    {
        enum Farver { rød, blå, gul, grøn, lilla, brun }
        static void Philip_MasterMind(string[] args)
        {
            byte[] kodeTal = new byte[4]; //Array der skal indeholde 4 tilfældigt generede tal, der skal bruges til gætte-koden.
            Farver[] kodeFarver = new Farver[kodeTal.Length];//Enum array, der indeholder de fire farver der udgør koden. 
            Farver[] spillerGaet = new Farver[kodeTal.Length];//Array, der skal indeholde spillerens gæt.
            Random rnd = new Random();
            int runde; //Variabel der gemmer hvad runde vi er i
            string highscore; //Variabel der gemmer highscore (skal indeholde Navn + runde)
            int highscoreRunde = 11; //Variabel der gemmer den highscore runden. 11, fordi der er 10 runder.
            bool stemningForSpil = true; //Bruges til at gemme om spilleren vil fortsætte 
            string inSvar; //Variabel der gemmer spillerens input, så de kan valideres
            int rigtigFarve = 0; //Variabel der skal gemme antal farver, uden rigtig plads, som spilleren har gættet
            int rigtigPlace = 0; //Variabel der skal gemme antal farver, på den rigtige plads, som spilleren har gættet
            bool[] placeBrugt = new bool[kodeTal.Length];
            bool[] gaetBrugt = new bool[kodeTal.Length];
            Farver[,] tidlGaet = new Farver[11, 4];//Array til at gemme spillerens gæt fra tidligere runder.
            int[,] tidlRes = new int[11, 2]; //Array til at gemme sorte/hvide fra tidligere runder

            do //Loop, der egentlig bare tager højde for om spilleren stadig vil spille.
               //Spilleren kan ændre mening efter runde 10, eller hvis de har vundet.
            {
                Velkomst();//Funktion til velkosmtbesked, fordi den er så lang.
                //For loop, der genererer random tal, på hver plads i kodeTal array
                for (byte i = 0; i < kodeTal.Length; i++)
                {
                    kodeTal[i] = (byte)rnd.Next(0, 6);
                }
                //Loop der indsætter de fire farver (fra tal i kodeTal) i kodeFarver arrayet
                for (int i = 0; i < kodeFarver.Length; i++)
                {
                    kodeFarver[i] = (Farver)kodeTal[i];
                }
                runde = 1; /*Sætter runde til 1. Man vender kun tilbage til dette loop,
                            hvis man har vundet, eller tabt, og valgt at fortsætte */
                /*
                 //Udskriver den kode der skal gættes til konsollen:
                 foreach (Farver farve in kodeFarver)
                     Console.WriteLine(farve);
                */

                Console.WriteLine("\nLad os først starte med dit navn. Hvad vil du gerne kaldes?");
                string spillerNavn = Console.ReadLine();
                Console.WriteLine($"\nHej med dig {spillerNavn}!\n" +
                    $"Tryk på en vilkårlig tast, for at begynde spillet!");
                Console.ReadKey();

                while (runde < 11) //Loop med selve spille, fra runde 1-10
                {
                    Console.Clear(); //Sletter konsollens indhold. BEMÆRK! KUN EN HVIS DEL, SVARENDE TIL HVAD DER KAN VÆRE I ET VINDUE!
                    if (runde > 1) //Skriver spillerens tidligere gæt og resultater for foregående runder, hvis nuvræende runde >1.
                    {
                        Philip_SkrivCyan("Tidligere gæt: ");
                        for (int i = 1; i < runde; i++)
                        {
                            Console.WriteLine("Runde " + i);
                            for (int j = 0; j < tidlGaet.GetLength(1); j++)
                            {
                                Philip_SkrivFarve(tidlGaet[i, j], Convert.ToString(tidlGaet[i, j]) + " ");
                            }
                            Console.WriteLine($"Du havde {tidlRes[i, 0]} sorte brikker og {tidlRes[i, 1]} hvide brikker.\n");
                        }
                    }
                    Philip_SkrivCyan("Runde " + runde);
                    for (int i = 0; i < kodeTal.Length; i++) //Loop hvor spilleren indtaster sine 4 (kodeTal.Length) gæt. 
                    {
                        Console.WriteLine("Hvilken farve gætter du på plads " + (i + 1) + "?" +
                            "\nHusk at du kan vælge imellem: Rød, Blå, Gul, Grøn, Lilla, Brun");
                        inSvar = Philip_GodkendSvar(); //Funktion der beder om input, kontrollerer at det er gyldigt og returere et gyldigt svar som string
                        spillerGaet[i] = (Farver)Enum.Parse(typeof(Farver), inSvar); //Svaret gemmes i array med spillerens gæt
                    }
                    Console.Clear();
                    Philip_SkrivCyan("Dit gæt er:\n");
                    for (int i = 0; i < kodeTal.Length; i++)
                    {
                        Philip_SkrivFarve(spillerGaet[i], spillerGaet[i] + " ");
                    }

                    //If statement, der tjekker om spilleren har vundet ved at gætte koden
                    if (spillerGaet[0] == kodeFarver[0] &&
                        spillerGaet[1] == kodeFarver[1] &&
                        spillerGaet[2] == kodeFarver[2] &&
                        spillerGaet[3] == kodeFarver[3])
                    {
                        Console.Clear();
                        //Besked med tillykke og score
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nTillykke du svarede rigtigt! \n" +
                            "Koden var: ");
                        Philip_SkrivFarve(kodeFarver[0], kodeFarver[0] + " ");
                        Philip_SkrivFarve(kodeFarver[1], kodeFarver[1] + " ");
                        Philip_SkrivFarve(kodeFarver[2], kodeFarver[2] + " ");
                        Philip_SkrivFarve(kodeFarver[3], kodeFarver[3] + " ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nDu gjorde på det på " + runde + " runder!");
                        if (runde < highscoreRunde) //Tjekker om man har slået highscoren
                        {
                            Console.WriteLine("Du har slået highscoren!");
                            highscore = "Highscoren er: " + Convert.ToString(runde) + ", den blev sat af: " + spillerNavn;
                            highscoreRunde = runde;
                            Console.WriteLine(highscore);
                        }
                        Console.ResetColor();
                        stemningForSpil = Philip_Afslut(); //Tjekker om de vil afslutte spillet
                        break; //Breaker ud, så man ender i "do...while" loopet
                    }
                    else //Håndtering af hvis spilleren ikke vandt med sit gæt
                    {
                        Console.WriteLine("\n\nDu gættede ikke rigtigt.\n");
                        //Loop, der kontrollere om spillerens gæt svarer til rigtige farver med rigtig palcering (hvid brik)
                        for (int i = 0; i < kodeFarver.Length; i++)
                        {
                            if (kodeFarver[i] == spillerGaet[i] && !placeBrugt[i])
                            {
                                rigtigPlace++;
                                placeBrugt[i] = true;
                                gaetBrugt[i] = true;
                            }
                        }
                        //Loop der kontrollerer om gættet svarer til en farve uden rigtig placering(sort brik)
                        //Hvid-loopet er kørt, så alle pladser hvor gættet er rigtigt placeret, er allerede udfyldt.
                        //Hvis gættet allerede har gættet en hvid, tælles det heller ikke igen.
                        for (int i = 0; i < spillerGaet.Length; i++)
                        {
                            for (int j = 0; j < kodeFarver.Length; j++)
                            {
                                if (spillerGaet[i] == kodeFarver[j] && !placeBrugt[j] && !gaetBrugt[i])
                                {
                                    rigtigFarve++;
                                    placeBrugt[j] = true;
                                    break;
                                }
                            }

                        }
                        //Feedback til spilleren, der svarer til de sorte/hvide brikker i klassisk MasterMind.
                        Console.WriteLine($"Du har {rigtigFarve} sort(e) brik(ker). Det er farve(r) der optræder i koden, men på den forkerte plads\n" +
                            $"og {rigtigPlace} hvid(e) brik(ker). Det er farver på den rigtige plads\n");
                        Philip_SkrivCyan("Tryk på en knap for at forsætte til næste runde");
                        Console.ReadKey();
                    }
                    if (runde == 10) //Hvis vi er i runde 10, er spillet slut og spilleren spørges om de vil afslutte eller forsætte
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du nåede desværre ikke at gætte koden inden for 10 runder.");
                        Console.ResetColor();
                        stemningForSpil = Philip_Afslut();
                    }
                    tidlGaet[runde, 0] = spillerGaet[0]; //Gemmer rundens gæt og resultat i tidlGaet array, på denne rundes plads.
                    tidlGaet[runde, 1] = spillerGaet[1];
                    tidlGaet[runde, 2] = spillerGaet[2];
                    tidlGaet[runde, 3] = spillerGaet[3];
                    tidlRes[runde, 0] = rigtigFarve;
                    tidlRes[runde, 1] = rigtigPlace;

                    rigtigFarve = 0; //Sætter rigtigFarve til 0, så det kun er den aktuelle rundes gæt der evalueres. 
                    rigtigPlace = 0; //Sætter rigtigPlace til 0, så det kun er den aktuelle rundes gæt der evalueres.
                    for (int i = 0; i < kodeTal.Length; i++) //Resetter de brugte gæt og placeringer
                    {
                        placeBrugt[i] = false;
                        gaetBrugt[i] = false;
                    }
                    runde++;
                }
                Console.Clear(); //Cleare konsollen, når et spil er afsluttet, efter sejr eller 10 runder
            } while (stemningForSpil);
        }

        /// <summary>
        /// En funktion, der skriver en velkomstbesked ud til konsollen, med forklaring af reglerne i MasterMind.
        /// </summary>
        static void Velkomst()
        {
            Philip_SkrivCyan(@"Velkommen til MasterMind!

Her er spillets regler:

Målet er at gætte den kode, som computeren har genereret, inden for 10 runder. 
Koden består af 4 farver. Farverne kan være: Rød, Blå, Gul, Grøn, Lilla eller Brun.

For hver runde skal du gætte på hvilke 4 farver du tror der er i koden. Du gætter på en plads ad gangen.
Vær opmærksom på at hver farve godt kan optræde flere gange i samme kode. 

Hvis du har gættet en farve der optræder i koden, men som ikke er på den rette plads,
fortæller spillet dig det. Det svarer til en sort brik, i den fysiske udgave af MasterMind.
Hvis du har gættet både rigtig farve og plads, fortæller spillet dig det. Det svarer til en hvid brik, i den fysiske udgave af MasterMind.
Du får ikke at vide på hvilken plads du har gættet rigtigt. 

Spillet slutter ved at du enten har gættet den rigtige kode, eller at der er gået 10 runder.

God fornøjelse!");
        }
        /// <summary>
        /// Funktion der spørger om spilleren vil afslutte. Hvis de vil, reutrnere funktionen false. Ellers returnere den true.
        /// </summary>
        /// <returns></returns>
        static bool Philip_Afslut()
        {
            bool afslutRes = true;
            Console.WriteLine("\nØnsker du at afslutte? Hvis ja: skriv \"quit\" og tryk enter.\nHvis du vil spille igen, tryk enter");
            if (Console.ReadLine().ToLower().Trim() == "quit")
            {
                afslutRes = false;
            }
            return afslutRes;

        }
        /// <summary>
        /// Lader spilleren skrive et input, og kontroller at det er et gyldigt svar. 
        /// Returnere en string, med det gyldige svar.
        /// </summary>
        /// <returns></returns>
        static string Philip_GodkendSvar()
        {
            string svar;
            while (true)
            {
                svar = Console.ReadLine()
                    .ToLower()
                    .Trim(); //fjerner whitespaces, f.eks. hvis spilleren er kommet til at trykke mellemrum før/efter

                if (svar == "rød")
                    return svar;
                else if (svar == "blå")
                    return svar;
                else if (svar == "gul")
                    return svar;
                else if (svar == "grøn")
                    return svar;
                else if (svar == "lilla")
                    return svar;
                else if (svar == "brun")
                    return svar;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDu har ikke indtastet et gyldigt svar.\nDu skal gætte på en af de nævnte farver\nPrøv igen:");
                    Console.ResetColor();
                }
            }
        }
        static void Philip_SkrivCyan(string tekst)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(tekst);
            Console.ResetColor();
        }
        static void Philip_SkrivFarve(Farver farve, string tekst)
        {
            switch (farve)
            {
                case Farver.blå:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.brun:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.grøn:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.gul:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.lilla:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;
                case Farver.rød:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(tekst);
                    Console.ResetColor();
                    break;

            }
        }
    }
}
