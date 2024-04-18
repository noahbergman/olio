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
            nuoli lol = new nuoli("", "", 100).valinta();
            Console.WriteLine("Tämän nuolen hinta on " + lol.Hinta + " kultaa");
        }

        public class nuoli
        {
            private paa paa;
            private hoyhen hoyhen;
            private int pituus;
            private double hinta = 0;

            public double Hinta
            {
                get { return hinta; }
                set { hinta = value; }
            }

            public paa Paa
            {
                get { return paa; }
                set { paa = value; }
            }

            public hoyhen Hoyhen
            {
                get { return hoyhen; }
                set { hoyhen = value; }
            }

            public int Pituus
            {
                get { return pituus; }
                set { pituus = value; }
            }

            public nuoli(string paa, string hoyhen, int pituus)
            {
                AsetaPaa(paa);
                AsetaHoyhen(hoyhen);
                AsetaPituus(pituus);
            }

            public nuoli valinta()
            {
                bool check = false;
                nuoli palautus = new nuoli("","",100);

                Console.WriteLine("Valitse nuoli: ");
                Console.WriteLine("");
                Console.WriteLine("1. Eliittinuoli (Timanttikärki, 100cm varsi, kotkansulka)");
                Console.WriteLine("2. Perusnuoli (Teräskärki, 85cm varsi, kanansulka)");
                Console.WriteLine("3. Aloittelijanuoli (Puukärki, 70cm varsi, lehti)");
                Console.WriteLine("4. Rakenna omasi");

                while (true)
                {
                    int x;
                    string vastaus = Console.ReadLine();

                    if (Int32.TryParse(vastaus, out x))
                    {
                        if (x == 1)
                        {
                            check = true;
                            palautus = nuoli.Eliittinuoli();
                        }

                        if (x == 2)
                        {
                            check = true;
                            palautus = nuoli.Perusnuoli();
                        }

                        if (x == 3)
                        {
                            check = true;
                            palautus = nuoli.Perusnuoli();
                        }

                        if (x == 4)
                        {
                            check = true;
                            palautus = new nuoli(kasaus1(), kasaus2(), kasaus3());
                        }
                    }

                    if (check)
                    {
                        return palautus;
                        break;
                    }

                    Console.WriteLine("Valitse annetuista vaihtoehdoista, kirjoita pelkkä numero");
                }
            }

            public static nuoli Eliittinuoli()
            {
                nuoli Eliittinuoli = new nuoli("timantti", "kotka", 100);
                return Eliittinuoli;
            }

            public static nuoli Perusnuoli()
            {
                nuoli Perusnuoli = new nuoli("teräs", "kana", 85);
                return Perusnuoli;
            }

            public static nuoli Aloittelijanuoli()
            {
                nuoli Aloittelijanuoli = new nuoli("puu", "lehti", 70);
                return Aloittelijanuoli;
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