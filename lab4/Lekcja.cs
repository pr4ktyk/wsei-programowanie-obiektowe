/*using System;

namespace lab4
{
    class Lekcja
    {
        enum Degree
        {
            BardzoDobry = 50,
            DobryPlus = 45,
            Dobry = 40,
            DostatecznyPlus= 35,
            Dostateczny = 30,
            Niedostateczny = 20
        }

        record Student(string Name, int Points, char Group);

        static void Main(string[] args)
        {
            Degree ocena = Degree.Dostateczny;
            Console.WriteLine(ocena);
            Console.WriteLine((int)ocena);
            string[] names = Enum.GetNames<Degree>();
            Console.WriteLine(string.Join(", ", names));
            Degree[] degrees = Enum.GetValues<Degree>();
            foreach (var item in degrees)
            {
                Console.WriteLine($"{item} ({(int)item})");
            }
            Console.WriteLine();
            Console.Write("Wpisz ocenę: ");
            string input = Console.ReadLine();

            try
            {
                Degree degree = Enum.Parse<Degree>(input);
                Console.WriteLine($"{degree} ({(int)degree})");
            } catch(ArgumentException ex)
            {
                Console.WriteLine($"Nie istnieje ocena \"{input}\"!");
            }

            string usDegree;
            switch (ocena)
            {
                case Degree.BardzoDobry:
                    usDegree = "A";
                    break;
                case Degree.DobryPlus:
                    usDegree = "B";
                    break;
            }

            usDegree = ocena switch
            {
                Degree.BardzoDobry => "A",
                Degree.DobryPlus => "B",
                Degree.Dobry => "C",
                Degree.DostatecznyPlus => "D",
                Degree.Dostateczny => "E",
                _ => "F"
            };

            Console.WriteLine($"US Degree : {usDegree}");

            int points = 45;

            Degree result;
            if (points >= 50 && points < 60)
            {
                result = Degree.Dostateczny;
            }

            result = points switch
            {
                >= 50 and < 60 => Degree.Dostateczny,
                >= 60 and < 70 => Degree.DostatecznyPlus,
                >= 70 and < 80 => Degree.Dobry,
                >= 80 and < 90 => Degree.DobryPlus,
                >= 90 and < 100 => Degree.BardzoDobry,
                _ => Degree.Niedostateczny
            };

            Student[] students =
            {
                new Student(Name: "Karol", Points: 80, Group: 'E'),
                new Student(Name: "Paweł", Points: 120, Group: 'E'),
                new Student(Name: "Ewa", Points: 70, Group: 'B'),
                new Student(Name: "Adam", Points: 60, Group: 'B'),
                new Student(Name: "Daniel", Points: 50, Group: 'E')
            };

            Console.WriteLine(students[1] == new Student("Paweł", 80, 'E'));

            foreach(Student st in students)
            {
                Console.WriteLine(st);
            }

            (string, bool)[] results = new (string, bool)[students.Length];
            for (int i = 0; i < students.Length; i++)
            {
                Student st = students[i];
                results[i] = (st.Name,
                    st switch
                    {
                        { Points: >= 100, Group: 'E' } => true,
                        { Points: >= 50, Group: 'B' } => true,
                        _ => false
                    });
            }

            foreach (var item in results)
            {
                Console.WriteLine($"{item.Item1} {(item.Item2 == true ? "zdał egzamin." : "nie zdał egzaminu.")}");
            }
        }
    }
}
*/