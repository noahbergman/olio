namespace sigma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            reppu lol = new reppu(30, 20, 10);

            lol.yeeah();
        }

        public class tavara
        {
            protected double paino;
            protected double tilavuus;

            public override string ToString()
            {
                string eh = base.ToString();
                eh = eh.Remove(0,14);
                return eh;
            }

            public double Paino
            {
                get { return paino; }
                set { paino = value; }
            }

            public double Tilavuus
            {
                get { return tilavuus; }
                set { tilavuus = value; }
            }

            public tavara(double paino, double tilavuus)
            {
                this.paino = paino;
                this.tilavuus = tilavuus;
            }
        }

        public class reppu
        {
            private double maxPaino;
            private double maxTilavuus;
            private int maxTavarat;
            private double currentPaino = 0;
            private double currentTilavuus = 0;
            private int currentTavarat = 0;
            private tavara[] sisältö;

            static nuoli Nuoli = new nuoli();
            static jousi Jousi = new jousi();
            static köysi Köysi = new köysi();
            static vesi Vesi = new vesi();
            static ruokaannos Ruokaannos = new ruokaannos();
            static miekka Miekka = new miekka();
            private tavara[] reff = { Nuoli, Jousi, Köysi, Vesi, Ruokaannos, Miekka };

            private string sisällä()
            {
                string e = "";

                if (sisältö.Length == 0)
                {
                    e = "Reppu on tyhjä";
                }

                else
                {
                    for (int i = 0; i < sisältö.Length; i++)
                    {
                        e += sisältö[i].ToString() + ", ";
                    }

                    e = e.Remove(e.Length - 2, 1);
                    e = "Repussa on: " + e;
                }

                return e;
            }

            public reppu(double maxPaino, double maxTilavuus, int maxTavarat)
            {
                this.maxPaino = maxPaino;
                this.maxTilavuus = maxTilavuus;
                this.maxTavarat = maxTavarat;

                sisältö = new tavara[0];
            }

            public void yeeah()
            {
                while (currentTavarat < maxTavarat)
                {
                    Console.WriteLine(sisällä());
                    Console.WriteLine($"Repussa on tällä hetkellä {currentTavarat}/{maxTavarat} tavaraa, {currentPaino}/{maxPaino} painoa, ja {currentTilavuus}/{maxTilavuus} tilaa");
                    Console.WriteLine("Mitä haluat lisätä?");
                    Console.WriteLine("1. Nuoli");
                    Console.WriteLine("2. Jousi");
                    Console.WriteLine("3. Köysi");
                    Console.WriteLine("4. Vettä");
                    Console.WriteLine("5. Ruokaa");
                    Console.WriteLine("6. Miekka");

                    while (true)
                    {
                        string vastaus = Console.ReadLine();
                        bool check = false;
                        int x;

                        if (Int32.TryParse(vastaus, out x))
                        {
                            for (int i = 0; i < x; i++)
                            {
                                if (i == x - 1)
                                {
                                    bool kek = lisää(reff[i]);

                                    if (kek == false)
                                    {
                                        Console.WriteLine("Esine ei mahtunut");
                                    }

                                    check = true;
                                }
                            }
                        }

                        if (check)
                        {
                            break;
                        }

                        Console.WriteLine("Valitse annetuista vaihtoehdoista, kirjoita pelkkä numero");
                    }
                }
            }

            bool lisää(tavara tavara)
            {
                if (currentPaino + tavara.Paino < maxPaino && currentTilavuus + tavara.Tilavuus < maxTilavuus)
                {
                    sisältö = sisältö.Append(tavara).ToArray();

                    currentPaino += tavara.Paino;
                    currentTilavuus += tavara.Tilavuus;
                    currentTavarat++;

                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public class nuoli : tavara
        {
            public nuoli() : base(0.1, 0.05) { }
        }

        public class jousi : tavara
        {
            public jousi() : base(1, 4) { }
        }

        public class köysi : tavara
        {
            public köysi() : base(1, 1.5) { }
        }

        public class vesi : tavara
        {
            public vesi() : base(2, 2) { }
        }

        public class ruokaannos : tavara
        {
            public ruokaannos() : base(1, 0.5) { }
        }

        public class miekka : tavara
        {
            public miekka() : base(5, 3) { }
        }
    }
}
