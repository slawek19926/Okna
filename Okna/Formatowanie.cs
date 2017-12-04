using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okna
{
    public class Formatowanie
    {
        private static string zero = "zero";
        private static string[] jednosci = { "", " jeden ", " dwa ", " trzy ",
        " cztery ", " pięć ", " sześć ", " siedem ", " osiem ", " dziewięć " };
        private static string[] dziesiatki = { "", " dziesięć ", " dwadzieścia ",
        " trzydzieści ", " czterdzieści ", " pięćdziesiąt ",
        " sześćdziesiąt ", " siedemdziesiąt ", " osiemdziesiąt ",
        " dziewięćdziesiąt "};
        private static string[] nascie = { "dziesięć", " jedenaście ", " dwanaście ",
        " trzynaście ", " czternaście ", " piętnaście ", " szesnaście ",
        " siedemnaście ", " osiemnaście ", " dziewiętnaście "};
        private static string[] setki = { "", " sto ", " dwieście ", " trzysta ",
        " czterysta ", " pięćset ", " sześćset ",
        " siedemset ", " osiemset ", " dziewięćset " };
        private static string[,] rzedy = {{" tysiąc ", " tysiące ", " tysięcy "},
                            {" milion ", " miliony ", " milionów "},
                            {" miliard ", " miliardy ", " miliardów "}};

        private static Dictionary<string, string[]> Waluty = new Dictionary<string, string[]>() {
        //Formy podawane według wzorca: jeden-dwa-pięć, np.
        //(jeden) złoty, (dwa) złote, (pięć) złotych
        { "PLN", new string[]{ "złoty", "złote", "złotych" } },
        { ".PLN", new string[]{ "grosz", "grosze", "groszy" } }
    };

        public static string LiczbaSlownie(int liczba)
        {
            return LiczbaSlownieBase(liczba).Replace("  ", " ").Trim();
        }

        public static string WalutaSlownie(int liczba, string kodWaluty)
        {
            var key = Waluty[kodWaluty];
            return key[DeklinacjaWalutyIndex(liczba)];
        }

        private static string LiczbaSlownieBase(int wartosc)
        {
            StringBuilder sb = new StringBuilder();
            //0-999
            if (wartosc == 0)
                return zero;
            int jednosc = wartosc % 10;
            int para = wartosc % 100;
            int set = (wartosc % 1000) / 100;
            if (para > 10 && para < 20)
                sb.Insert(0, nascie[jednosc]);
            else
            {
                sb.Insert(0, jednosci[jednosc]);
                sb.Insert(0, dziesiatki[para / 10]);
            }
            sb.Insert(0, setki[set]);

            //Pozostałe rzędy wielkości
            wartosc = wartosc / 1000;
            int rzad = 0;
            while (wartosc > 0)
            {
                jednosc = wartosc % 10;
                para = wartosc % 100;
                set = (wartosc % 1000) / 100;
                bool rzadIstnieje = wartosc % 1000 != 0;
                if ((wartosc % 1000) / 10 == 0)
                {
                    //nie ma dziesiątek i setek
                    if (jednosc == 1)
                        sb.Insert(0, rzedy[rzad, 0]);
                    else if (jednosc >= 2 && jednosc <= 4)
                        sb.Insert(0, rzedy[rzad, 1]);
                    else if (rzadIstnieje)
                        sb.Insert(0, rzedy[rzad, 2]);
                    if (jednosc > 1)
                        sb.Insert(0, jednosci[jednosc]);
                }
                else
                {
                    if (para >= 10 && para < 20)
                    {
                        sb.Insert(0, rzedy[rzad, 2]);
                        sb.Insert(0, nascie[para % 10]);
                    }
                    else
                    {
                        if (jednosc >= 2 && jednosc <= 4)
                            sb.Insert(0, rzedy[rzad, 1]);
                        else if (rzadIstnieje)
                            sb.Insert(0, rzedy[rzad, 2]);
                        sb.Insert(0, jednosci[jednosc]);
                        sb.Insert(0, dziesiatki[para / 10]);
                    }
                    sb.Insert(0, setki[set]);
                }
                rzad++;
                wartosc = wartosc / 1000;
            }
            return sb.ToString();
        }

        private static int DeklinacjaWalutyIndex(int liczba)
        {
            if (liczba == 1)
                return 0;

            int para = liczba % 100;
            if (para >= 10 && para < 20)
                return 2;

            int jednosc = liczba % 10;
            if (jednosc >= 2 && jednosc <= 4)
                return 1;

            return 2;
        }
    }
}
