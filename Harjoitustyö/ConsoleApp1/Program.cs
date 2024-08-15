using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static ConsoleApp1.Program;
using static System.Formats.Asn1.AsnWriter;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pelaaja pelaaja = new Pelaaja(1, "test");
            pelaaja.setBase();

            pelaaja.inventory.Add(new Potion());
            pelaaja.inventory.Add(new SuperPotion());
            pelaaja.inventory.Add(new Potion());
            pelaaja.inventory.Add(new Potion());
            pelaaja.inventory.Add(new Potion());
            hub(pelaaja);
        }

        static public void hub(Pelaaja pelaaja)
        {
            pelaaja.setStats(pelaaja);
            hubMain();

            void hubMain()
            {
                Console.Clear();
                Console.WriteLine(pelaaja.inventory[1].slot);
                Console.WriteLine("Mitä haluat tehdä?");
                Console.WriteLine();
                Console.WriteLine("1. Taistelu");
                Console.WriteLine("2. Kauppa");
                Console.WriteLine("3. Status");
                Console.WriteLine("4. Varusteet");
                Console.WriteLine();
                int input = tarkistaVastaus(1, 4, Console.ReadLine());

                switch (input)
                {
                    case 1:
                        taisteluValinta();
                        break;

                    case 2:
                        Kauppa.kauppa(pelaaja);
                        break;

                    case 3:
                        status();
                        break;

                    case 4:
                        equip1();
                        break;
                }
            }

            void taisteluValinta()
            {
                Console.Clear();

                Console.WriteLine("Mitä vastaan haluat taistella?");
                Console.WriteLine();
                Console.WriteLine("1. Lima");
                Console.WriteLine("2. Peikko");
                Console.WriteLine("3. Rosvo");
                Console.WriteLine("4. Lohikäärme");
                Console.WriteLine("0. Takaisin");
                int input = tarkistaVastaus(0, 4, Console.ReadLine());

                switch (input)
                {
                    case 1:
                        pelaaja.Taistelu(new Lima(), pelaaja);
                        break;

                    case 2:
                        pelaaja.Taistelu(new Peikko(), pelaaja);
                        break;

                    case 3:
                        pelaaja.Taistelu(new Rosvo(), pelaaja);
                        break;

                    case 4:
                        pelaaja.Taistelu(new Lohikäärme(), pelaaja);
                        break;

                    case 0:
                        hubMain();
                        break;
                }
            }

            void status()
            {
                Console.Clear();

                Console.WriteLine($"{pelaaja.NIMI}");
                Console.WriteLine();
                Console.WriteLine($"Level: {pelaaja.LEVEL}");
                Console.WriteLine($"XP: {pelaaja.XP}/{pelaaja.xpn[pelaaja.LEVEL]}");
                Console.WriteLine();
                Console.WriteLine($"HP: {pelaaja.HP}");
                Console.WriteLine($"Strength: {pelaaja.STR}");
                Console.WriteLine($"Defense: {pelaaja.DEF}");
                Console.WriteLine($"Speed: {pelaaja.SPD}");
                Console.WriteLine();
                Console.WriteLine($"Kulta: {pelaaja.GOLD}");
                Console.WriteLine();

                Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                Console.ReadKey();
                hubMain();
            }

            void equip1()
            {
                Console.Clear();

                Console.WriteLine($"1. Pää");
                Console.WriteLine($"2. Keho");
                Console.WriteLine($"3. Housut");
                Console.WriteLine($"4. Ase");
                Console.WriteLine("5. Takaisin");

                int input = tarkistaVastaus(1, 5, Console.ReadLine());

                switch (input)
                {
                    case 1:
                        equip2(Slot.paa, 0);
                        break;

                    case 2:
                        equip2(Slot.keho, 1);
                        break;

                    case 3:
                        equip2(Slot.housut, 2);
                        break;

                    case 4:
                        equip2(Slot.ase, 3);
                        break;

                    case 5:
                        hubMain();
                        break;
                }
            }

            void equip2(Slot slot, int equipSlot)
            {
                Console.Clear();

                int list = 0;
                List<int> slotIndex = new List<int>();

                foreach (var l9 in pelaaja.inventory)
                {
                    if (l9.slot == slot)
                    {
                        list++;
                        Console.WriteLine($"{list}. {l9.nimi}");
                        slotIndex.Add(pelaaja.inventory.IndexOf(l9));
                    }
                }

                string print = $"1-{list - 1}. Esine      0. Takaisin";

                if (list == 1)
                {
                    print = "1. Esine      0. Takaisin";
                }

                else if (list == 0)
                {
                    print = "Ei yhtään vastaavaa esinettä      0. Takaisin";
                }

                Console.WriteLine();
                Console.WriteLine(print);
                Console.WriteLine();

                int input = tarkistaVastaus(0, list, Console.ReadLine());

                if (input == 0)
                {
                    equip1();
                }

                else
                {
                    pelaaja.loadout[equipSlot] = pelaaja.inventory[slotIndex[input - 1]];
                    equip1();
                }

                pelaaja.setStats(pelaaja);
            }
        }

        public static int tarkistaVastaus(int min, int max, string vastaus)
        {
            int x = 0;

            while (true)
            {
                bool check = int.TryParse(vastaus, out int vastausInt);

                if (check)
                {
                    if (vastausInt <= max && vastausInt >= min)
                    {
                        x = vastausInt;
                        break;
                    }
                }

                Console.WriteLine("Valitse numero");
                vastaus = Console.ReadLine();
            }
            return x;
        }

        public enum Slot { paa, keho, housut, ase, consumable }

        public class Pelaaja
        {
            protected int hp = 10;
            protected int str = 4;
            protected int def = 0;
            protected int spd = 10;
            protected int level = 1;
            protected int xp = 6;
            protected int gold = 100000;
            protected string nimi;
            protected int invcapacity = 20;
            public int currentHP;
            public int currentHPV;

            int baseHP;
            int baseSTR;
            int baseDEF;
            int baseSPD;

            public void setBase()
            {
                baseHP = hp;
                baseSTR = str;
                baseDEF = def;
                baseSPD = spd;
            }

            public void setStats(Pelaaja pelaaja)
            {
                pelaaja.hp = baseHP;
                pelaaja.str = baseSTR;
                pelaaja.def = baseDEF;

                foreach (var var in pelaaja.inventory)
                {
                    pelaaja.def += var.def;
                    pelaaja.str += var.str;
                }
            }

            Random random = new Random();

            static Slot[] slots = new Slot[]
            {
                Slot.paa,
                Slot.keho,
                Slot.housut,
                Slot.ase
            };
            public Esine[] loadout = new Esine[slots.Length];
            public Ase equippedAse;
            public List<Esine> inventory = new List<Esine>();

            public bool checkSpace(List<Esine> inv, Esine x, int capacity)
            {
                if (inventory.Count < capacity)
                {
                    inv.Add(x);
                    return true;
                }

                else
                {
                    return false;
                }
            }

            public void myy(Esine x)
            {
                GOLD += x.myyntihinta;
                inventory.Remove(x);
            }


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

            public int INVCAPACITY
            {
                get { return invcapacity; }
                set {  invcapacity = value; }
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

            public void Taistelu(Vihollinen vihollinen, Pelaaja pelaaja)
            {
                currentHP = hp;
                currentHPV = vihollinen.HP;
                bool voitto = false;
                bool häviö = false;
                bool taistelu = false;
                int turn = 1;
                vihollinen.abilityPassive(pelaaja);

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

                        int input = tarkistaVastaus(1, 4, Console.ReadLine());

                        switch (input)
                        {
                            case 1:
                                hyökkää();
                                break;

                            case 2:
                                reppu();
                                break;

                            case 3:
                                tarkista();
                                break;

                            case 4:
                                pakene();
                                break;
                        }
                    }
                }

                void reppu()
                {
                    Console.Clear();

                    int z = 1;
                    List<int> xIndex = new List<int>();
                    List<Consumable> tempConsumable = new List<Consumable>();

                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("Reppu on tyhjä");
                        Console.WriteLine();
                    }                    

                    else
                    {
                        foreach (Consumable x in inventory)
                        {
                            Console.WriteLine($"{z}. {x.nimi}");

                            tempConsumable.Add(x);

                            xIndex.Add(inventory.IndexOf(x));

                            z++;
                        }

                        string x2 = $"1-{z}";

                        if (z == 1)
                        {
                            x2 = "1";
                        }

                        Console.WriteLine();
                        Console.WriteLine($"0. Takaisin               {x2}. Esine");
                        Console.WriteLine();
                        int input = tarkistaVastaus(1, z, Console.ReadLine());

                        if (input == 0)
                        {
                            valinta();
                        }

                        else
                        {
                            tempConsumable[input - 1].use(pelaaja, vihollinen);
                            inventory.RemoveAt(xIndex[input-1]);

                            // Pelaaja pelaaja, int current, Vihollinen vihollinen, int vihollinenHP
                        }
                    }

                    Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                    Console.ReadKey();

                    valinta();
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
                        häviö = true;
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

                        CheckLevelup(vihollinen.XP, pelaaja);

                        Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                        Console.ReadKey();

                        hub(pelaaja);
                    }

                    if (häviö)
                    {
                        Console.Clear();

                        Console.WriteLine("Hävisit ja menetit puolet kullastasi");
                        Console.WriteLine();
                        Console.WriteLine($"Kulta: {pelaaja.gold} -> {pelaaja.gold / 2}");
                        Console.WriteLine();

                        pelaaja.gold = pelaaja.gold / 2;

                        Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                        Console.ReadKey();

                        hub(pelaaja);
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
                    vihollinen.abilityAttack(pelaaja);
                    currentHP -= vihollinen.STR;
                }

                valinta();
            }

            // level up tarkistus ja suoritus //

            public void CheckLevelup(int vihxp, Pelaaja pelaaja)
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
                    Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                    Console.ReadKey();

                    xp -= xpn[level];
                    level++;
                    baseHP += 3;
                    baseSTR += 2;
                    baseDEF += 1;
                    baseSPD += 1;
                }

                else
                {
                    Console.WriteLine($"XP: {xp}/{xpn[level]} -> {xp + vihxp}/{xpn[level]}");
                }

                xp += vihxp;

                hub(pelaaja);
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
                set { desc = value; }
            }

            public Vihollinen(int hp, int str, int def, int spd, int xp, string nimi, int goldmin, int goldmax)
            {
                this.hp = hp;
                this.str = str;
                this.def = def;
                this.spd = spd;
                this.xp = xp;
                this.nimi = nimi;
                this.gold = random.Next(goldmin, goldmax + 1);
            }

            public abstract void abilityHit(Pelaaja pelaaja);
            public abstract void abilityAttack(Pelaaja pelaaja);
            public abstract void abilityPassive(Pelaaja pelaaja);
        }

        // eri vihollis tyyppejä //

        public class Lima : Vihollinen
        {
            public Lima() : base(10, 2, 0, 4, 5, "Lima", 10, 20)
            {
                desc = new string[]
                {
                    "Liman lyöminen hidastaa sinua"
                };
            }

            public override void abilityHit(Pelaaja pelaaja)
            {
                pelaaja.SPD -= 2;
            }

            public override void abilityAttack(Pelaaja pelaaja) { }
            public override void abilityPassive(Pelaaja pelaaja) { }
        }

        public class Peikko : Vihollinen
        {
            public Peikko() : base(15, 4, 2, 7, 15, "Peikko", 15, 30)
            {
                string[] desc = new string[]
                {
                    "Peikon aura saa sinut tärisemään, heikentäen hyökkäyksiäsi"
                };
            }

            public override void abilityHit(Pelaaja pelaaja) { }
            public override void abilityAttack(Pelaaja pelaaja) { }
            public override void abilityPassive(Pelaaja pelaaja)
            {
                pelaaja.STR -= 3;
            }

        }

        public class Rosvo : Vihollinen
        {
            public Rosvo() : base(20, 8, 5, 25, 40, "Rosvo", 100, 150)
            {
                string[] desc = new string[]
                {
                    "Sinuun hyökätessä rosvo varastaa hieman kultaa"
                };
            }

            public override void abilityHit(Pelaaja pelaaja) { }
            public override void abilityAttack(Pelaaja pelaaja)
            {
                Console.Clear();

                var rand = new Random();

                int lost = pelaaja.GOLD / 100 * rand.Next(3);
                pelaaja.GOLD -= lost;

                Console.WriteLine($"Rosvo varasti {lost} kultaa");
                Console.WriteLine($"Kulta:{pelaaja.GOLD + lost} -> {pelaaja.GOLD}");
                Console.WriteLine();
                Console.WriteLine("Paina mitätahansa nappia jatkaaksesi");
                Console.ReadKey();
            }
            public override void abilityPassive(Pelaaja pelaaja) { }
        }

        public class Lohikäärme : Vihollinen
        {
            public Lohikäärme() : base(100, 40, 30, 70, 1000, "Lohikäärme", 2000, 4000)
            {
                string[] desc = new string[]
                {
                    "Placeholder"
                };
            }

            public override void abilityHit(Pelaaja pelaaja) { }
            public override void abilityAttack(Pelaaja pelaaja) { }
            public override void abilityPassive(Pelaaja pelaaja) { }
        }


        public class Esine
        {
            public Slot slot;
            public int str;
            public int hp;
            public int def;
            public virtual void kauppaDesc(bool myy) { }
            public string nimi;
            public int hinta;
            public int myyntihinta;
        }

        public class Equip : Esine { }

        public class Ase : Equip
        {

            public override void kauppaDesc(bool myy)
            {
                Console.WriteLine(nimi);
                Console.WriteLine($"Strength: {str}");
                Console.WriteLine();

                switch (myy)
                {
                    case true:
                        Console.WriteLine($"Saat: {myyntihinta} kultaa");
                        break;

                    case false:
                        Console.WriteLine($"Hinta: {hinta} kultaa");
                        break;

                }
            }

            public Ase(Slot slot, int str, string nimi, int hinta)
            {
                this.slot = slot;
                this.str = str;
                this.nimi = nimi;
                this.hinta = hinta;
                this.myyntihinta = Convert.ToInt32(Math.Floor(Convert.ToDouble(hinta) * 0.7));
            }
        }

        public class Armor : Equip
        {
            protected int def;
            protected int hp;

            public override void kauppaDesc(bool myy)
            {
                Console.WriteLine(nimi);
                Console.WriteLine($"Defense: {def}");
                Console.WriteLine($"HP: {hp}");
                Console.WriteLine();

                switch (myy)
                {
                    case true:
                        Console.WriteLine($"Saat: {myyntihinta} kultaa");
                        break;

                    case false:
                        Console.WriteLine($"Hinta: {hinta} kultaa");
                        break;

                }
            }

            public Armor(Slot slot, int def, int hp, string nimi, int hinta)
            {
                this.slot = slot;
                this.def = def;
                this.hp = hp;
                this.nimi = nimi;
                this.hinta = hinta;
                this.myyntihinta = Convert.ToInt32(Math.Floor(Convert.ToDouble(hinta) * 0.7));
            }
        }

        public class Dagger : Ase
        {
            public Dagger() : base(Slot.ase, 3, "Tikari", 10) { }
        }

        public class Sword : Ase
        {
            public Sword() : base(Slot.ase, 10, "Miekka", 50) { }
        }

        public class WarAxe : Ase
        {
            public WarAxe() : base(Slot.ase, 25, "Sotakirves", 50) { }
        }

        public class Morningstar : Ase
        {
            public Morningstar() : base(Slot.ase, 50, "Aamutähti", 200) { }
        }

        public class Wand : Ase
        {
            public Wand() : base(Slot.ase, 70, "Taikasauva", 500) { }
        }

        public class LeatherCap : Armor
        {
            public LeatherCap() : base(Slot.paa, 2, 0, "Nahkalakki", 3) { }
        }

        public class LeatherVest : Armor
        {
            public LeatherVest() : base(Slot.keho, 3, 0, "Nahkaliivi", 5) { }
        }

        public class LeatherPants : Armor
        {
            public LeatherPants() : base(Slot.housut, 3, 0, "Nahkahousut", 5) { }
        }

        public class ChainMail : Armor
        {
            public ChainMail() : base(Slot.keho, 8, 0, "Rengashaarniska", 25) { }
        }

        public class PlateMail : Armor
        {
            public PlateMail() : base(Slot.keho, 25, 0, "Levyhaarniska", 125) { }
        }

        public class PlateMailL : Armor
        {
            public PlateMailL() : base(Slot.housut, 20, 0, "Levyhaarniska (jalat)", 100) { }
        }

        public abstract class Consumable : Esine
        {
            public Consumable(Slot slot, string nimi, int hinta)
            {
                this.slot = slot;
                this.nimi = nimi;
                this.hinta = hinta;
                this.myyntihinta = Convert.ToInt32(Math.Floor(Convert.ToDouble(hinta) * 0.7));
            }

            public abstract void use(Pelaaja pelaaja, Vihollinen vihollinen);
        }

        public class Potion : Consumable
        {
            int healing = 10;

            public Potion() : base(Slot.consumable, "Parannus juoma", 5) { }

            public override void use(Pelaaja pelaaja, Vihollinen vihollinen)
            {
                if (pelaaja.currentHP + healing <= pelaaja.HP)
                {
                    pelaaja.currentHP += healing;
                    Console.WriteLine("1123314131231567778976511");
                }

                else if (pelaaja.HP - pelaaja.currentHP >= 0)
                {
                    pelaaja.currentHP = pelaaja.HP;
                    Console.WriteLine("1qwesdaseqweqetyy67778976511");
                }
            }
        }

        public class SuperPotion : Consumable
        {
            int healing = 25;

            public SuperPotion() : base(Slot.consumable, "Super parannus juoma", 25) { }

            public override void use(Pelaaja pelaaja, Vihollinen vihollinen)
            {
                if (pelaaja.currentHP + healing <= pelaaja.HP)
                {
                    pelaaja.currentHP += healing;
                }

                else if (pelaaja.HP - pelaaja.currentHP >= 0)
                {
                    pelaaja.currentHP = pelaaja.HP;
                }
            }
        }

        public class DamagePotion : Consumable
        {
            int damage = 10;

            public DamagePotion() : base(Slot.consumable, "Taika räjähde", 25) { }

            public override void use(Pelaaja pelaaja, Vihollinen vihollinen)
            {
                pelaaja.currentHPV -= damage;
            }
        }

        public class Kauppa
        {
            static Esine[] valikoima = new Esine[]
            {
                new Dagger(),
                new Sword(),
                new WarAxe(),
                new Morningstar(),
                new Wand(),
                new LeatherCap(),
                new LeatherVest(),
                new LeatherPants(),
                new ChainMail(),
                new PlateMail(),
                new PlateMailL(),
                new Potion(),
                new SuperPotion(),
                new DamagePotion()
            };

            public static void kauppa(Pelaaja pelaaja)
            {
                void valinta()
                {
                    Console.Clear();
                    Console.WriteLine("Mitä haluaisit tehdä?");
                    Console.WriteLine();
                    Console.WriteLine("1. Osta");
                    Console.WriteLine("2. Myy");
                    Console.WriteLine("3. Poistu");

                    int input = tarkistaVastaus(1, 3, Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            osta1();
                            break;

                        case 2:
                            myy1();
                            break;

                        case 3:
                            Console.Clear();
                            hub(pelaaja);
                            break;
                    }
                }

                void myy1()
                {
                    Console.Clear();
                    int z = 1;

                    foreach (Esine x in pelaaja.inventory)
                    {
                        Console.WriteLine($"{z}. {x.nimi}");
                        Console.WriteLine();
                        z++;
                    }

                    Console.WriteLine("0. Takaisin               1-9. Esine");
                    Console.WriteLine();
                    int input = tarkistaVastaus(0, z, Console.ReadLine());

                    if (input == 0)
                    {
                        valinta();
                    }

                    Console.Clear();

                    pelaaja.inventory[input - 1].kauppaDesc(true);

                    Console.WriteLine();
                    Console.WriteLine("1. Myy               2. Takaisin");

                    int input2 = tarkistaVastaus(1, 2, Console.ReadLine());

                    switch (input2)
                    {
                        case 1:
                            pelaaja.myy(pelaaja.inventory[input - 1]);
                            myy1();
                            break;

                        case 2:
                            myy1();
                            break;
                    }
                }

                void osta1()
                {
                    Console.Clear();

                    Console.WriteLine("1. Aseet");
                    Console.WriteLine("2. Panssarit ja asusteet");
                    Console.WriteLine("3. Kertakäyttöesineet ja taikajuomat");
                    Console.WriteLine("4. Poistu");
                    Console.WriteLine();

                    int input = tarkistaVastaus(1, 4, Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            osta2(typeof(Ase));
                            break;

                        case 2:
                            osta2(typeof(Armor));
                            break;

                        case 3:
                            osta2(typeof(Consumable));
                            break;

                        case 4:
                            valinta();
                            break;
                    }
                }

                void osta2(Type type)
                {
                    Console.Clear();
                    int z = 1;
                    List<int> eh = new List<int>();

                    foreach (Esine x in valikoima)
                    {
                        if (x.GetType().IsSubclassOf(type))
                        {
                            eh.Add(Array.IndexOf(valikoima, x));
                            Console.WriteLine($"{z}. {x.nimi}");
                            Console.WriteLine();
                            z++;
                        }
                    }

                    int y;
                    Console.WriteLine($"Kulta: {pelaaja.GOLD}");
                    Console.WriteLine("0. Takaisin               1-9. Esine");
                    Console.WriteLine();

                    int input = tarkistaVastaus(0, z, Console.ReadLine());

                    if (input == 0)
                    {
                        osta1();
                    }

                    Console.Clear();

                    valikoima[eh[input - 1]].kauppaDesc(false);

                    Console.WriteLine();
                    Console.WriteLine("1. Osta               2. Takaisin");

                    int input2 = tarkistaVastaus(1, 2, Console.ReadLine());

                    switch (input2)
                    {
                        case 1:
                            osta3(valikoima[eh[input - 1]], type);
                            break;

                        case 2:
                            osta2(type);
                            break;
                    }
                }

                void osta3(Esine x, Type type)
                {
                    Console.Clear();
                    if (pelaaja.GOLD >= x.hinta)
                    {
                        if (pelaaja.checkSpace(pelaaja.inventory, x, pelaaja.INVCAPACITY))
                        {
                            pelaaja.GOLD -= x.hinta;
                        }

                        else
                        {
                            Console.WriteLine("Sinulla ei ole tarpeeksi tilaa");
                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        Console.WriteLine("Sinulla ei ole tarpeeksi rahaa");
                        Console.ReadKey();
                    }

                    osta2(type);
                }

                valinta();
            }
        }
    }
}