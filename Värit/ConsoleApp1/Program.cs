namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VarietyTavara<Tavara> swag1 = new VarietyTavara<Tavara>(new Miekka(), ConsoleColor.Blue);
            VarietyTavara<Tavara> swag2 = new VarietyTavara<Tavara>(new Kirves(), ConsoleColor.Red);
            VarietyTavara<Tavara> swag3 = new VarietyTavara<Tavara>(new Jousi(), ConsoleColor.Green);

            swag1.NäytäTavara();
            swag2.NäytäTavara();
            swag3.NäytäTavara();
        }

        public class VarietyTavara<T>
        {
            public T tavara;
            public ConsoleColor väri;

            public VarietyTavara(T Tavara, ConsoleColor Väri)
            {
                this.tavara = Tavara;
                this.väri = Väri;
            }

            public void NäytäTavara()
            {
                string kirjoita = tavara.ToString();
                kirjoita = kirjoita.Remove(0, 20);

                Console.ForegroundColor = väri;
                Console.WriteLine(kirjoita);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public class Tavara
        {

        }

        public class Miekka : Tavara
        {

        }

        public class Kirves : Tavara
        {

        }

        public class Jousi : Tavara
        {

        }
    }
}