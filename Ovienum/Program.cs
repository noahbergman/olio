namespace Ovienum
{
    internal class Program
    {
        static void lukitus()
        {
            Ovilukitus current = Ovilukitus.Auki;

            while(true)
            {
                Console.WriteLine("Mitä haluaisit tehdä ovelle? ");

                string vastaus = Console.ReadLine();

                if(vastaus == "sulje")
                {
                    if(current != Ovilukitus.Auki)
                    {
                        Console.WriteLine("Ovi ei ole auki");
                    }

                    else
                    {
                        Console.WriteLine("Ovi on nyt kiinni");
                        current = Ovilukitus.Kiinni;
                    }
                }

                else if(vastaus == "lukitse")
                {
                    if(current == Ovilukitus.Auki)
                    {
                        Console.WriteLine("Ovi ei ole edes kiinni");
                    }

                    else if(current == Ovilukitus.Lukossa)
                    {
                        Console.WriteLine("Ovi on jo lukossa");
                    }

                    else
                    {
                        Console.WriteLine("Ovi on nyt lukossa");
                        current = Ovilukitus.Lukossa;
                    }
                }

                else if(vastaus == "avaa lukko")
                {
                    if(current == Ovilukitus.Kiinni)
                    {
                        Console.WriteLine("Ovi ei ole Lukossa");
                    }

                    else if(current == Ovilukitus.Auki)
                    {
                        Console.WriteLine("Ovi ei ole edes kiinni");
                    }

                    else
                    {
                        Console.WriteLine("Lukko on nyt auki");
                        current = Ovilukitus.Kiinni;
                    }
                }

                else if(vastaus == "avaa")
                {
                    if(current == Ovilukitus.Lukossa)
                    {
                        Console.WriteLine("Ovi on lukossa");
                    }

                    else if(current == Ovilukitus.Auki)
                    {
                        Console.WriteLine("Ovi on jo auki");
                    }

                    else
                    {
                        Console.WriteLine("Ovi on nyt auki");
                        current = Ovilukitus.Auki;
                    }

                }
                
                    else
                    {
                        Console.WriteLine("avaa, sulje, lukitse tai avaa lukko");
                    }
            }
        }
        
        static void Main(string[] args)
        {
            lukitus();
        }

        enum Ovilukitus { Auki, Kiinni, Lukossa};
    }
}