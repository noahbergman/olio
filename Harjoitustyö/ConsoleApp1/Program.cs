using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pelaaja test = new Pelaaja(1, "test");
            Console.WriteLine(test.xpn.Length);
            test.Taistelu(new Lima());
            Console.WriteLine(test.xpn.Length);
        }

        public enum Slot { paa, keho, housut, kengät, hanskat, misc, ase, consumable }
        public enum Damagetype { blunt, slash, pierce, magic , na }

        public class Pelaaja
        {
            // level 1 stats, hp on max hp eikä nykyinen //
            protected int hp = 10;
            protected int str = 4;
            protected int def = 0;
            protected int spd = 10;
            protected int level = 1;
            protected int xp = 6;
            protected int gold = 3;
            protected string nimi;
            Random random = new Random();

            static Slot[] slots = new Slot[]
            {
                Slot.paa,
                Slot.keho,
                Slot.housut,
                Slot.kengät,
                Slot.hanskat,
                Slot.misc,
                Slot.ase
            };
            Equip[] loadout = new Equip[slots.Length];
            Esine[] inventory = new Esine[20];

            public int HP
            {
                get { return hp; }
                set { hp = value; }
            }

            public int STR
            {
                get { return str; }
                set { str = value; }
            }

            public int DEF
            {
                get { return def; }
                set { def = value; }
            }

            public int SPD
            {
                get { return spd; }
                set { spd = value; }
            }

            public int LEVEL
            {
                get { return level; }
                set { level = value; }
            }

            public int XP
            {
                get { return xp; }
                set { xp = value; }
            }

            public int GOLD
            {
                get { return gold; }
                set { gold = value; }
            }

            public string NIMI
            {
                get { return nimi; }
                set { nimi = value; }
            }

            // tarvittava xp per level //
            public int[] xpn = new int[100];

            public Pelaaja(int level, string nimi)
            {
                this.level = level;
                this.nimi = nimi;

                // asetetaan tarvittava xp määrä per level //
                double x = 10;
                for (int i = 1; i < xpn.Length; i++)
                {
                    xpn[i] = (int)x;

                    x = Math.Floor(x * 1.1);
                }
            }

            public void Taistelu(Vihollinen vihollinen)
            {
                int currentHP = hp;
                int currentHPV = vihollinen.HP;
                bool voitto = false;
                bool taistelu = false;
                int turn = 1;

                Console.WriteLine("sasdasd");

                // valitset mitä teet //
                void valinta()
                {
                    if (taistelu == false)
                    {
                        Console.Clear();
                        Console.WriteLine($"Turn {turn}.   {nimi}: {currentHP}/{hp}        {vihollinen.NIMI}: {currentHPV}/{vihollinen.HP}");
                        Console.WriteLine();
                        Console.WriteLine("1. Hyökkää");
                        Console.WriteLine("2. Reppu");
                        Console.WriteLine("3. Vihollinen");
                        Console.WriteLine("4. Pakene");
                        Console.WriteLine();

                        string valitse = Console.ReadLine();

                        switch (valitse)
                        {
                            case "1":
                                hyökkää();
                                break;

                            case "3":
                                tarkista();
                                break;

                            case "4":
                                pakene();
                                break;
                        }
                    }
                }

                // nopeampi hyökkää ensin //

                void hyökkää()
                {
                    turn++;
                    if (spd > vihollinen.SPD)
                    {
                        Hyökkäys();

                        tarkistahäviö();

                        VHyökkäys();

                        tarkistahäviö();
                    }

                    else if (spd < vihollinen.SPD)
                    {
                        VHyökkäys();

                        tarkistahäviö();

                        Hyökkäys();

                        tarkistahäviö();
                    }

                    Console.WriteLine(voitto);
                    result();
                    valinta();
                }

                // ns. inspect menu //

                void tarkista()
                {
                    Console.Clear();
                    Console.WriteLine($"{vihollinen.NIMI}:");
                    Console.WriteLine();
                    Console.WriteLine($"{currentHPV}/{vihollinen.HP}HP");
                    Console.WriteLine($"Strength: {vihollinen.STR}");
                    Console.WriteLine($"Defense: {vihollinen.DEF}");
                    Console.WriteLine($"Speed: {vihollinen.SPD}");
                    Console.WriteLine();
                    for ( int i = 0; i < vihollinen.DESC.Length; i++)
                    {
                        Console.WriteLine(vihollinen.DESC[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                    Console.ReadKey();

                    valinta();
                }

                // Tarkistaa kuoliko kumpikaan joka hyökkäyksen jälkeen //

                void tarkistahäviö()
                {
                    if (currentHP <= 0)
                    {
                        voitto = false;
                        taistelu = true;
                        valinta();
                    }

                    else if (currentHPV <= 0)
                    {
                        voitto = true;
                        taistelu = true;
                        valinta();
                    }
                }

                // end screen taistelun jälkeen //

                void result()
                {
                    if (voitto)
                    {
                        Console.Clear();

                        Console.WriteLine("Voitto");
                        Console.WriteLine();

                        CheckLevelup(vihollinen.XP);

                        Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                        Console.ReadKey();
                    }
                }

                // arpoo pakenemisen nopeuden ja tuurin perusteella // 

                void pakene()
                {
                    turn++;
                    int x = random.Next(1, 7) * spd;
                    int y = random.Next(1, 7) * vihollinen.SPD;

                    if (x > y)
                    {
                        Console.Clear();
                        Console.WriteLine("Pakenit Onnistuneesti");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                        Console.ReadKey();
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{x} {y}");
                        Console.WriteLine("Pakeneminen epäonnistui, käytit vuorosi ja vihollinen hyökkäsi sinuun");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                        Console.ReadKey();

                        VHyökkäys();

                        tarkistahäviö();
                        valinta();
                    }
                }

                // hyökkäykset //

                void Hyökkäys()
                {
                    currentHPV -= str;
                }

                void VHyökkäys()
                {
                    currentHP -= vihollinen.STR;
                }

                valinta();
            }

            // level up tarkistus ja suoritus //

            public void CheckLevelup(int vihxp)
            {
                if (xp + vihxp > xpn[level])
                {
                    Console.WriteLine($"Level up: {level} -> {level + 1}");
                    Console.WriteLine($"XP: {xp}/{xpn[level]} -> {xp + vihxp - xpn[level]}/{xpn[level + 1]}");
                    Console.WriteLine();
                    Console.WriteLine($"HP: {hp} -> {hp + 3}");
                    Console.WriteLine($"Strength: {str} -> {str + 2}");
                    Console.WriteLine($"Defense: {def} -> {def + 1}");
                    Console.WriteLine($"Speed: {spd} -> {spd + 1}");
                    Console.WriteLine();

                    xp -= xpn[level];
                    level++;
                    hp += 3;
                    str += 2;
                    def += 1;
                    spd += 1;
                }

                else
                {
                    Console.WriteLine($"XP: {xp}/{xpn[level]} -> {xp + vihxp}/{xpn[level]}");
                }
            }
        }

        public abstract class Vihollinen
        {
            // hp on max hp eikä nykyinen, xp annetaan pelaajalle taistelun voitettuaan //
            protected int hp;
            protected int str;
            protected int def;
            protected int spd;
            protected int xp;
            protected int gold;
            protected string nimi;
            protected Damagetype heikkous;
            protected Damagetype kestää;
            Random random = new Random();
            protected internal string[] desc = new string[4];

            public int HP
            {
                get { return hp; }
                set { hp = value; }
            }

            public int STR
            {
                get { return str; }
                set { str = value; }
            }

            public int DEF
            {
                get { return def; }
                set { def = value; }
            }

            public int SPD
            {
                get { return spd; }
                set { spd = value; }
            }

            public int XP
            {
                get { return xp; }
                set { xp = value; }
            }

            public int GOLD
            {
                get { return gold; }
                set { gold = value; }
            }
            public string NIMI
            {
                get { return nimi; }
                set { nimi = value; }
            }

            public string[] DESC
            {
                get { return desc; }
                set {  desc = value; }
            }

            public Vihollinen(int hp, int str, int def, int spd, int xp, string nimi, int goldmin, int goldmax, Damagetype heikkous, Damagetype kestää)
            {
                this.hp = hp;
                this.str = str;
                this.def = def;
                this.spd = spd;
                this.xp = xp;
                this.nimi = nimi;
                this.gold = random.Next(goldmin, goldmax + 1);
                this.heikkous = heikkous;
                this.kestää = kestää;
            }
        }

        // eri vihollis tyyppejä //

        public class Lima : Vihollinen
        {
            public Lima() : base(10, 2, 0, 4, 5, "Lima", 10, 20, Damagetype.slash, Damagetype.blunt)
            {
                desc = new string[]
                {
                    "Placeholder",
                    "",
                    $"Heikkous: {heikkous}",
                    $"Kestää: {kestää}"
                };
            }
        }

        public class Peikko : Vihollinen
        {
            public Peikko() : base(15, 4, 2, 7, 15, "Peikko", 15, 30, Damagetype.na, Damagetype.na)
            {
                string[] desc = new string[]
                {
                    "Placeholder",
                    "",
                    $"Heikkous: {heikkous}",
                    $"Kestää: {kestää}"
                };
            }
        }

        public class Rosvo : Vihollinen
        {
            public Rosvo() : base(20, 8, 5, 25, 40, "Rosvo", 100, 150, Damagetype.na, Damagetype.na)
            {
                string[] desc = new string[]
                {
                    "Placeholder",
                    "",
                    $"Heikkous: {heikkous}",
                    $"Kestää: {kestää}"
                };
            }
        }

        public class Lohikäärme : Vihollinen
        {
            public Lohikäärme() : base(100, 40, 30, 70, 1000, "Lohikäärme", 2000, 4000, Damagetype.na, Damagetype.na)
            {
                string[] desc = new string[]
                {
                    "Placeholder",
                    "",
                    $"Heikkous: {heikkous}",
                    $"Kestää: {kestää}"
                };
            }
        }


        public class Esine { }

        public class Equip : Esine
        {
            Slot slot;
            Damagetype damagetype;
            protected int str;
            protected int def;
            protected int hp;
            protected string nimi;

            public Equip(Slot slot, Damagetype damagetype, int str, string nimi, int hinta)
            {
                this.slot = slot;
                this.damagetype = damagetype;
                this.str = str;
                this.nimi = nimi;
            }

            public Equip(int def, int hp, string nimi)
            {
                this.def = def;
                this.hp = hp;
                this.nimi = nimi;
            }
        }

        public class Dagger : Equip
        {
            public Dagger() : base(Slot.ase, Damagetype.pierce, 0, "Tikari") { }
        }

        public class Bow : Equip
        {
            public Bow() : base(Slot.ase, Damagetype.pierce, 6, "Jousipyssy") { }
        }

        public abstract class Consumable : Esine
        {
            protected string nimi;

            public Consumable(string nimi)
            {
                this.nimi = nimi;
            }

            public abstract void use(Pelaaja pelaaja, int current, Vihollinen vihollinen, int vihollinenHP);
        }

        public class Potion : Consumable
        {
            int healing = 10;

            public Potion() : base("Parannus juoma") { }

            public override void use(Pelaaja pelaaja, int current, Vihollinen vihollinen, int vihollinenHP)
            {
                if (current + healing <= pelaaja.HP)
                {
                    current += healing;
                }

                else if (pelaaja.HP - current > 0)
                {
                    current += pelaaja.HP - current;
                }
            }          
        }

        public class Kauppa
        {
            public void kauppa(Pelaaja pelaaja)
            {
                void valinta()
                {
                    Console.WriteLine("Mitä haluaisit tehdä?");
                    Console.WriteLine();
                    Console.WriteLine("1. Osta");
                    Console.WriteLine("2. Myy");
                    Console.WriteLine("3. Poistu");

                    string valitse = Console.ReadLine();

                    switch (valitse)
                    {
                        case "1":
                            break;
                    }
                }


            }
        }
    }
}