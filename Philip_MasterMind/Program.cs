using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.DesignerServices;
using System.Text;
using System.Threading.Tasks;

namespace Philip_MasterMind
{
    internal class Program
    {
        enum Farver { rød, blå, gul, grøn, lilla, brun}
        static void Main(string[] args)
        {
            /* TODO:
             * Feed back på om man har rigtig farve, men ikke plads
             * Feedback på om man har rigtig farve OG plads
             */

            byte[] kodeTal = new byte[4]; //Array der skal indeholde 4 tilfældigt generede tal, der skal bruges til gætte-koden
            Farver[] kodeFarver = new Farver[4];//Enum array, der indeholder de fire farver der udgør koden
            Farver[] spillerGaet = new Farver[4];//Array, der skal indeholde spillerens gæt
            Random rnd = new Random();
            int runde; //Variabel der gemmer hvad runde vi er i
            string highscore; //Variabel der gemmer highscore (skal indeholde Navn + runde)
            int highscoreRunde = 11; //Variabel der gemmer den highscore runden. 11, fordi der er 10 runder.
            bool stemningForSpil = true; //Bruges til at gemme om spilleren vil fortsætte 
            string inSvar; //Variabel der gemmer spillerens input, så de kan valideres

            Velkomst();

            do //Loop, der egentlig bare tager højde for om spilleren stadig vil spille.
               //Spilleren kan ændre mening efter runde 10, eller de har vundet
            {
                //For loop, der genererer 4 random tal, på hver plads i kodeTal array
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

                //SLET IGEN:
                foreach (Farver farve in kodeFarver)
                    Console.WriteLine(farve);


                Console.WriteLine("\nLad os først starte med dit navn. Hvad vil du gerne kaldes?");
                string spillerNavn = Console.ReadLine();
                Console.WriteLine($"\nHej med dig {spillerNavn}, nu er det tid til 1. runde!\n");

                while (runde < 11) //Loop med selve spille, fra runde 1-10
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Runde:" + runde);
                    Console.ResetColor();
                    Console.WriteLine("Hvilken farve gætter du på plads 1?\nHusk at du kan vælge imellem: Rød, Blå, Gul, Grøn, Lilla, Brun");
                    inSvar = GodkendSvar();
                    spillerGaet[0] = (Farver)Enum.Parse(typeof(Farver), inSvar);
                    Console.WriteLine("Hvilken farve gætter du på plads 2?"); 
                    inSvar = GodkendSvar();
                    spillerGaet[1] = (Farver)Enum.Parse(typeof(Farver), inSvar);
                    Console.WriteLine("Hvilken farve gætter du på plads 3?");
                    inSvar = GodkendSvar();
                    spillerGaet[2] = (Farver)Enum.Parse(typeof(Farver), inSvar);
                    Console.WriteLine("Hvilken farve gætter du på plads 4?");
                    inSvar = GodkendSvar();
                    spillerGaet[3] = (Farver)Enum.Parse(typeof(Farver), inSvar);

                    Console.WriteLine("\n\nDit gæt er:\n");
                    Console.WriteLine(spillerGaet[0] + " | " + spillerGaet[1] + " | " + spillerGaet[2] + " | " + spillerGaet[3] + "\n");

                    //If statement, der tjekker om spilleren har gættet koden
                    if (spillerGaet[0] == kodeFarver[0] && spillerGaet[1] == kodeFarver[1] &&
    spillerGaet[2] == kodeFarver[2] && spillerGaet[3] == kodeFarver[3])
                    {
                        //Besked med tillykke og score
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tillykke du svarede rigtigt! \nKoden var: " + kodeFarver[0] + " | " + kodeFarver[1] + " | " + kodeFarver[2] + " | " + kodeFarver[3] + "\n");
                        Console.WriteLine("Du gjorde på det på " + runde + " runder!");
                        if (runde < highscoreRunde) //Tjekker om man har slået highscoren
                        {
                            Console.WriteLine("Du har slået highscoren!");
                            highscore = "Highscoren er: " + Convert.ToString(runde) + ", den blev sat af: " + spillerNavn;
                            highscoreRunde = runde;
                            Console.WriteLine(highscore);
                        }
                        Console.ResetColor();
                        stemningForSpil = Afslut(); //Tjekker om de vil afslutte spillet
                        break; //Breaker ud, så man ender i "do...while" loopet
                    }
                    else
                    {
                        Console.WriteLine("Du gættede ikke rigtigt.\n");
                    }

                    if (runde ==10) //Hvis vi er i runde 10, spørges spilleren om de vil afslutte eller forsætte
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du nåede desværre ikke at gætte koden inden for 10 runder.");
                        Console.ResetColor();
                        stemningForSpil = Afslut();
                    }

                    runde++;

                }
            } while (stemningForSpil);
        }

        /// <summary>
        /// En funktion, der skriver en velkomstbesked ud til konsollen, med forklaring af reglerne i MasterMind.
        /// </summary>
        static void Velkomst()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"Velkommen til MasterMind!

Her er spillets regler:

Målet er at gætte den kode, som computeren har genereret, inden for 10 runder. 
Koden består af 4 farver. Farverne kan være: Rød, Blå, Gul, Grøn, Lilla eller Brun.

For hver runde skal du gætte på hvilke 4 farver du tror der er i koden. Du gætter på en plads ad gangen.
Vær opmærksom på at hver farve godt kan optræde flere gange i samme kode. 

Hvis du har gættet en farve der optræder i koden, men som ikke er på den rette plads,
viser spillet dig det med en sort brik. Hvis du har gættet både rigtig farve og plads,
viser spillet en hvid plads. Du får ikke at vide på hvilken plads du har gættet rigtigt. 

Spillet slutter ved at du enten har gættet den rigtige kode, eller at der er gået 10 runder.

God fornøjelse!");
            Console.ResetColor();
        }
        /// <summary>
        /// Funktion der spørger om spilleren vil afslutte. Hvis de vil, reutrnere funktionen false. Ellers returnere den true.
        /// </summary>
        /// <returns></returns>
        static bool Afslut()
        {
            bool afslutRes = true;
            Console.WriteLine("Ønsker du at afslutte? Hvis ja: skriv \"quit\" og tryk enter.\nHvis du vil fortsætte, tryk enter");
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
        static string GodkendSvar()
        {
            string svar;
            while(true)
            {
                svar = Console.ReadLine()
                    .ToLower()
                    .Trim(); //fjerner whitespaces, f.eks. hvis spilleren er kommet til at trykke mellemrød

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

    }
}
