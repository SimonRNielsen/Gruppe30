using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Jeopardy
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Jeopardy, lavet af Irene
            #region Noter
            /*
            //string springOver = "Meld pas";

            Console.BackgroundColor = ConsoleColor.Blue;

            #region Intro
            Console.WriteLine("Velkommen til Jeopardy! Hvor du stiller spørgsmålene og vi har Svarene!" + "\n" + "Vi har 6 katogorier med 5 beløb i hver. Gætter man rigtigt, må man vælge et nyt beløb indenfor samme kategori");

            //Klassisk Jeopardy er med 3 spillere. 
            Console.WriteLine("Spiller 1, input dit navn:");
            string spillerEt = Console.ReadLine();

            Console.WriteLine("Spiller 2, input dit navn:");
            string spillerTo = Console.ReadLine();

            Console.WriteLine("Spiller 3, input dit navn:");
            string spillerTre = Console.ReadLine();

            //Hvilken spiller starter, genereres med Random class
            Random rng = new Random();
            int randomNumber1 = rng.Next(0, 4);
            Console.WriteLine($"Computeren vælger tilfældigt hvem der skal starte: Spiller {randomNumber1}");
            Console.ReadKey();

            #endregion

            //Kategori Array
            Console.WriteLine("Dagens kategorier er:");
            //Kategori[] = new string[6, 6] {{ "aBasisprogrammering", "aSvar1", "aSvar2", "aSvar3", "aSvar4", "aSvar5" }, { "bFlowControl", "bSvar1", "bSvar2", "bSvar3", "bSvar4", "bSvar5"}, { "cFunktioner", "cSvar1", "cSvar2", "cSvar3", "cSvar4", "cSvar5"}, { "dArrays", "dSvar1", "dSvar2", "dSvar3", "dSvar4", "dSvar5"}, { "eSpildesign", "eSvar1", "eSvar2", "eSvar3", "eSvar4", "eSvar5"}, { "fVirksomhed", "fSvar1", "fSvar2", "fSvar3", "fSvar4", "fSvar5"} };

            //Console.WriteLine(baseGrid(0, 6, 12, 18, 24, 36));

            //Kald array
            /*for (int x = 0; x < skakArray.GetLength(0); x++)
             {
            for (int y = 0; y < skakArray.GetLength(1); y++)
            {
            Console.Write(skakArray[x, y] + " ");*/


            /*
            //breaker loops op så vi får rækker i boardet
            Console.Write("\n");

            while (randomNumber1 = ;)
                {
                Console.WriteLine("Input et bogstav for din valgte kategori og til hvilken værdi, du vil have:");
                Console.WriteLine("Kategori:");
                Console.ReadLine();
                Console.WriteLine("Beløb:");
                Console.ReadLine();

                Console.WriteLine("Husk at ende dit spørgsmål med '?'");
                Console.WriteLine("Korrekt! Du må vælge en ny kategori og en ny værdi");
                }
            */

            //#region Vinder / afslut
            ////Make into "highest"
            ///*int currentSmallest = int.MaxValue; // Start higher than anything in the array.
            //for (int index = 0; index<array.Length; index++)
            //{
            //if (array[index] < currentSmallest)
            //currentSmallest = array[index];
            //}
            //Console.WriteLine(currentSmallest);*/
            //Console.WriteLine("Spiller X vinder med xxx point"); //køre array igennem for at finde højeste score
            //int[] array = new int[] {};

            //#endregion
            #endregion

            string spillerNavne = ();


            string kategorier = ();
            string[] kategorier = new string[] { "KatA", "KatB", "KatC", "KatD", "KatE", "KatF" }

            int[] beløbA = new int[] { 200, 400, 600, 800, 1000 }
            int[] beløbB = new int[] { 200, 400, 600, 800, 1000 }
            int[] beløbC = new int[] { 200, 400, 600, 800, 1000 }
            int[] beløbD = new int[] { 200, 400, 600, 800, 1000 }
            int[] beløbE = new int[] { 200, 400, 600, 800, 1000 }
            int[] beløbF = new int[] { 200, 400, 600, 800, 1000 }


            #region Start/intro

            //Intro
            Console.WriteLine("Velkommen til Jeopardy! Hvor du stiller spørgsmålene og vi har Svarene!" + "\n" + "Vi har 6 katogorier med 5 beløb i hver. Gætter man rigtigt, må man vælge et nyt beløb indenfor samme kategori");

            //Antal spillere + navne
            Console.WriteLine("Man kan max være 3 spillere. Hvor mange spillere er I?");
            int antalSpillere = Convert.ToInt32(Console.ReadLine());

            if (antalSpillere > 0 || antalSpillere < 4)
            {
                Console.WriteLine("Spiller 1, input dit navn:");
                string spillerEt = Console.ReadLine(); spillerNavne = 1


            Console.WriteLine("Spiller 2, input dit navn:");
                string spillerTo = Console.ReadLine(); spillerNavne = 2


            Console.WriteLine("Spiller 3, input dit navn:");
                string spillerTre = Console.ReadLine(); spillerNavne = 3


            string[] spillerNavne = new string[antalSpillere] { spillerEt, spillerTo, spillerTre };
            }

            else
            {
                Console.WriteLine("Du har ikke indtastet et gyldigt nummer, prøv igen.");
            }

            #endregion



            Console.ReadKey();
        }
    }
}
