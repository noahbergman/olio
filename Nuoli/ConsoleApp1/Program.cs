using System.Security.Cryptography.X509Certificates;
using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    
    internal class Program
    {
        static string kasaus1()
        {
            bool check = false;
            string vastaus1;
            var paat = new[] { "puu", "teras", "timantti" };

            while (true)
            {
                Console.Write("Minkälainen kärki (puu, teräs, timantti)?:  ");
                vastaus1 = Console.ReadLine();
                vastaus1.ToLower();

                foreach (string x in paat)
                {
                    if (vastaus1 == x)
                    {
                        check = true;
                        break;
                    }
                }

                if (check)
                {
                    check = false;
                    break;
                }
                Console.WriteLine("Valitse annetuista vaihtoehdoista");
            }

            return vastaus1;

        }

        static string kasaus2()
        {
            bool check = false;
            string vastaus2;
            var hoyhenet = new[] { "lehti", "kana", "kotka" };

            while (true)
            {
                Console.Write("Minkälainen höyhen (lehti, kana, kotka)?:  ");
                vastaus2 = Console.ReadLine();
                vastaus2.ToLower();

                foreach (string x in hoyhenet)
                {
                    if (vastaus2 == x)
                    {
                        check = true;
                        break;
                    }
                }

                if (check)
                {
                    return vastaus2;
                    break;
                }
                Console.WriteLine("Valitse annetuista vaihtoehdoista");
            }
        }

        static int kasaus3()
        {
            bool check = false;
            string vastaus3 = "";

            while (true)
            {
                Console.Write("Kuinka pitkä varsi (60-100cm)?:  ");
                vastaus3 = Console.ReadLine();

                int x = 0;

                if (Int32.TryParse(vastaus3, out x))
                {
                    if (x <= 100 && x >= 60)
                    {
                        check = true;
                    }
                }

                if (check)
                {
                    check = false;
                    return x;
                    break;
                }
                Console.WriteLine("Valitse annetulta väliltä (kirjoita pelkkä numero, jätä cm pois");
            }
        }


        static void Main(string[] args)
        {
            nuoli valmis = new nuoli(kasaus1(), kasaus2(), kasaus3());
            Console.WriteLine("Tämän nuolen hinta on " + valmis.hinta + " kultaa");
        }

        public class nuoli
        {
            private paa paa;
            private hoyhen hoyhen;
            private int pituus;
            public double hinta = 0;

            public nuoli(string paa, string hoyhen, int pituus)
            {
                AsetaPaa(paa);
                AsetaHoyhen(hoyhen);
                AsetaPituus(pituus);
            }

            public void AsetaPaa(string paa)
            {
                if (paa == "puu")
                {
                    this.paa = Program.paa.puu;
                    this.hinta += 3;
                }

                else if (paa == "teras")
                {
                    this.paa = Program.paa.teras;
                    this.hinta += 5;
                }

                else if (paa == "timantti")
                {
                    this.paa = Program.paa.timantti;
                    this.hinta += 50;
                }
            }

            public void AsetaHoyhen(string hoyhen)
            {
                if (hoyhen == "lehti")
                {
                    this.hoyhen = Program.hoyhen.lehti;
                }

                else if (hoyhen == "kana")
                {
                    this.hoyhen = Program.hoyhen.kana;
                    this.hinta += 1;
                }

                else if (hoyhen == "kotka")
                {
                    this.hoyhen = Program.hoyhen.kotka;
                    this.hinta += 5;
                }
            }

            public void AsetaPituus(int pituus)
            {
                this.pituus = pituus;
                this.hinta += pituus * 0.05;
            }
        }

        public enum paa { puu, teras, timantti };
        public enum hoyhen { lehti, kana, kotka };
    }
}