namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public class Pelaaja
        {
            protected int hp;
            protected int str;

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

        }

        public class Vihollinen
        {
            protected int hp;
            protected int str;

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

        }

        public class Esine
        {

        }

        public class Kauppa
        {

        }
    }
}