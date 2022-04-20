using System;

namespace lab7
{

    delegate double Operator(double a, double b);

    class Program
    {

        public static double Addition(double x, double y)
        {
            return x + y;
        }

        public static double Multiply(double x, double y)
        {
            return x * y;
        }

        public static void PrintIntArray(int[] arr, Func<int, string> formatter)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(formatter.Invoke(item));
            }
        }

        static void Main(string[] args)
        {
            Operator operation = Addition;
            double result = operation.Invoke(3, 6);
            Console.WriteLine($"{result}");

            operation = Multiply;
            result = operation.Invoke(2, 4);
            Console.WriteLine($"{result}");

            Func<double, double, double> op = Multiply;
            result = op.Invoke(3, 5);
            Console.WriteLine($"{result}");

            // Zwraca wartosc podanego typu (tutaj string)
            Func<int, string> Formatter = delegate (int number)
            {
                return String.Format("0x{0:X}", number);
            };

            Func<int, string> DecFormat = delegate (int number)
            {
                return String.Format("{0}", number);
            };

            Console.WriteLine(Formatter.Invoke(18));
            PrintIntArray(new int[]{10, 18, 155}, Formatter);
            PrintIntArray(new int[]{10, 18, 155}, DecFormat);

            // zwraca zawsze bool
            Predicate<string> OnlyThreeChars = delegate(string s)
            {
                return s.Length == 3;
            };

            Func<int, int, int, bool> InRange = delegate (int val, int min, int max)
            {
                return (val > min && val < max);
            };

            // Nie zwraca nic, wykonuje polecenie
            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };

            // zwraca wynik dodawania, nazwy argumentów w nawiasie, po => działanie
            Operator AddLambda = (a, b) => a + b;

            // zwraca int, nie ma argumentu (trzeba wstawić pusty nawias)
            Func<int> Lambda = () => 5;

            Action<string> PrintLambda = s => Console.WriteLine(s);

            // drugi argument PrintIntArray przekazany jako lambda
            PrintIntArray(new int[] { 3, 15, 255 }, n => string.Format("0x{0:X}", n));
        }
    }
}
