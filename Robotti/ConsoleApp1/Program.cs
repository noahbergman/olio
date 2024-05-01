using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Robotti sigma = new Robotti();

            for (int i = 0; i < sigma.Käskyt.Length; i++)
            {
                Console.WriteLine("Mitä komentoja syötetään robotille?: Käynnistä, Sammuta, Ylös, Alas, Oikea, Vasen");
                bool lolz = true;

                while (lolz == true)
                {
                    string lol = Console.ReadLine();

                    switch (lol.ToLower())
                    {
                        case "sammuta":
                            Sammuta sammuta = new Sammuta();
                            sigma.Käskyt[i] = sammuta; lolz = false; break;

                        case "käynnistä":
                            Käynnistä käynnistä = new Käynnistä();
                            sigma.Käskyt[i] = käynnistä; lolz = false; break;

                        case "ylös":
                            YlösKäsky ylös = new YlösKäsky();
                            sigma.Käskyt[i] = ylös; lolz = false; break;

                        case "alas":
                            AlasKäsky alas = new AlasKäsky();
                            sigma.Käskyt[i] = alas; lolz = false; break;

                        case "oikea":
                            OikeaKäsky oikea = new OikeaKäsky();
                            sigma.Käskyt[i] = oikea; lolz = false; break;

                        case "vasen":
                            VasenKäsky vasen = new VasenKäsky();
                            sigma.Käskyt[i] = vasen; lolz = false; break;

                        default:
                            Console.WriteLine("Kirjoita pelkkä sana");
                            lolz = true; break;
                    }
                }
            }

            sigma.Suorita();
        }

        public class Robotti
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool OnKäynnissä { get; set; }
            public RobottiKäsky?[] Käskyt { get; } = new RobottiKäsky?[5];

            public void Suorita()
            {
                foreach (RobottiKäsky? käsky in Käskyt)
                {
                    käsky?.Suorita(this);
                    Console.WriteLine($"[{X} {Y} {OnKäynnissä}]");
                }
            }
        }

        public abstract class RobottiKäsky
        {
            public abstract void Suorita(Robotti robotti);
        }

        public class Käynnistä : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                robotti.OnKäynnissä = true;
            }
        }

        public class Sammuta : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                robotti.OnKäynnissä = false;
            }
        }

        public class YlösKäsky : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                if (robotti.OnKäynnissä == true)
                {
                    robotti.Y++;
                }              
            }
        }

        public class AlasKäsky : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                if (robotti.OnKäynnissä == true)
                {
                    robotti.Y--;
                }
            }
        }

        public class OikeaKäsky : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                if (robotti.OnKäynnissä == true)
                {
                    robotti.X++;
                }
            }
        }

        public class VasenKäsky : RobottiKäsky
        {
            public override void Suorita(Robotti robotti)
            {
                if (robotti.OnKäynnissä == true)
                {
                    robotti.X--;
                }
            }
        }
    }
}