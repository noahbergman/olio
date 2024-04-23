namespace ruoka
{
    internal class Program
    {
        static void tilaus()
        {
            Console.WriteLine("Kokoa annoksesi: ");

            Console.Write("Pääraaka-aine  (nautaa, kanaa, kasviksia): ");
            string vastaus = Console.ReadLine();

            Console.Write("Lisuke  (riisiä, perunaa, pastaa): ");
            string vastaus2 = Console.ReadLine();

            Console.Write("Kastike  (curry, pippuri, hapanimelä, chili): ");
            string vastaus3 = Console.ReadLine();
        }

        enum paa {nautaa, kanaa, kasviksia }
        enum lisuke {riisiä, perunaa, pastaa}
        enum kastike {curry, pippuri, hapanimela, chili}
    }
}