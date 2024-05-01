namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Coordinate Kappa = new Coordinate(4, 1);
            Coordinate Kappamantis = new Coordinate(3, 73);
            Coordinate Kappachungus = new Coordinate(4, 2);

            Console.WriteLine(Kappachungus.ViereinenPiste(Kappa));
            Console.WriteLine(Kappachungus.ViereinenPiste(Kappamantis));
        }

        struct Coordinate
        {
            public int x;
            public int y;

            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public bool ViereinenPiste(Coordinate kordi)
            {
                bool xVieressa = MathF.Abs(x - kordi.x) == 1 && y == kordi.y;
                bool yVieressa = MathF.Abs(y - kordi.y) == 1 && x == kordi.x;

                return xVieressa || yVieressa;
            }
        }
    }
}