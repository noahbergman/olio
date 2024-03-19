using System;
namespace ConsoleApp1
{
    internal class Program
    {
        static void valinta()
        {
            Console.WriteLine("Kokoa annos");

            paa maindish = paa.Kanaa;
            lisuke sidedish = lisuke.Riisiä;
            kastike sauce = kastike.Pippuri;

            while(true)
            {
                Console.Write("Pääraaka-aine: Kanaa, Nautaa, Kasviksia   ");
                string tarkistus = Console.ReadLine();
                tarkistus.ToLower();

                if (tarkistus == "kanaa")
                {
                    maindish = paa.Kanaa;
                    break;
                }

                else if (tarkistus == "nautaa")
                {
                    maindish = paa.Nautaa;
                    break;
                }

                else if (tarkistus == "kasviksia")
                {
                    maindish = paa.Kasviksia;
                    break;
                }

                Console.WriteLine("Valitse annetuista vaihtoehdoista");
            }

            while(true)
            {
                Console.Write("Lisuke: Riisiä, Pastaa, Perunaa   ");
                string tarkistus = Console.ReadLine();
                tarkistus.ToLower();

                if (tarkistus == "riisiä")
                {
                    sidedish = lisuke.Riisiä;
                    break;
                }

                else if (tarkistus == "pastaa")
                {
                    sidedish = lisuke.Pastaa;
                    break;
                }

                else if (tarkistus == "perunaa")
                {
                    sidedish = lisuke.Perunaa;
                    break;
                }

                Console.WriteLine("Valitse annetuista vaihtoehdoista");
            }

            while(true)
            {
                Console.Write("Kastike: Curry, Tomaatti, Pippuri   ");
                string tarkistus = Console.ReadLine();
                tarkistus.ToLower();

                if (tarkistus == "curry")
                {
                    sauce = kastike.Curry;
                    break;
                }

                else if (tarkistus == "pippuri")
                {
                    sauce = kastike.Pippuri;
                    break;
                }

                else if (tarkistus == "tomaatti")
                {
                    sauce = kastike.Tomaatti;
                    break;
                }

                Console.WriteLine("Valitse annetuista vaihtoehdoista");
            }

            Tuple<paa, lisuke, kastike> annos = new Tuple <paa, lisuke, kastike>(maindish, sidedish, sauce);

            Console.WriteLine(annos.Item1.ToString() + " ja " + annos.Item2.ToString() + " " + annos.Item3.ToString() + "kastikkeella");
        }

        static void Main(string[] args)
        {
            valinta();
        }

        enum paa {Kanaa, Nautaa, Kasviksia};
        enum lisuke {Riisiä, Pastaa, Perunaa};
        enum kastike {Pippuri, Curry, Tomaatti};
    }
}