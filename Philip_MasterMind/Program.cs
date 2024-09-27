using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philip_MasterMind
{
    internal class Program
    {
        enum Farver { Rød, Blå, Gul, Grøn, Lilla, Brun}
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //MasterMind Spil
            int rndTal = rnd.Next(0, 6);// Genererer et tilfældigt tal ml. 0-5 og gemmer i rndTal
           
            
            
            /* Velkomst();

            Console.WriteLine("\nLad os først starte med dit navn. Hvad vil du gerne kaldes?\n");
            string spillerNavn = Console.ReadLine();

            Console.WriteLine($"Hej med dig {spillerNavn}, nu er det tid til 1. runde!");
           */

            Console.ReadKey();
        }

        /// <summary>
        /// EN funktion, der skriver en velkomstbesked ud til konsollen, med forklaring af reglerne.
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
    }
}
