using System;
using System.Collections.Generic;
using System.Linq;

namespace lab8
{
    class Program
    {
        record Student(string Name, char Team, int Ects);

        static void Main(string[] args)
        {
            int[] ints = { 3, 6, 1, 4, 7, 8, 3, 4, 9, 12 };

            Predicate<int> oddPredicate = (n) =>
            {
                Console.WriteLine($"Obliczanie predykatu dla {n}.");
                return n % 2 == 1;
            };
            Console.WriteLine("Przed wykonaniem przypisania:");
            IEnumerable<int> odds = from n in ints
                                    where oddPredicate.Invoke(n)
                                    select n;
            Console.WriteLine("Po wykonaniu przypisania:");
            odds = from n in odds
                   where n > 5
                   select n;


            //parametryzowane
            int limit = 5;

            odds = from n in odds
                   where n % 2 == 1 && n > limit
                   select n;

            Console.WriteLine(string.Join(", ", odds));


            string[] strings = { "aa", "ee","bb", "ccc", "fff", "ss", "ggggg" };
            //zapisz wyrażenie LINQ, które zwróci listę łańcuchów o długości 2 znaków
            IEnumerable<string> doubleDigitWords = from s in strings
                                                   select s.Length.ToString();

            IEnumerable<string> sortStringsByAlphabet = from s in strings
                                                        orderby s
                                                        select s;

            IEnumerable<string> sortStringsByLength = from s in strings
                                                      orderby s.Length
                                                      select s;

            Console.WriteLine(string.Join(", ", doubleDigitWords));
            Console.WriteLine(string.Join(", ", sortStringsByLength));
            Console.WriteLine(string.Join(", ", sortStringsByAlphabet));

            //zapisz wyrażenie, które podnosi do kwadratu każdą liczbę z ints i posortuje wyniki malejąco
            Console.WriteLine(string.Join(", ",
                                                from n in ints
                                                orderby Math.Pow(n, 2) descending
                                                select Math.Pow(n, 2)));

            Student[] students =
            {
                new Student("Ania", 'A', 20),
                new Student("Bartek", 'B', 10),
                new Student("Czesław", 'A', 30),
                new Student("Dagmara", 'B', 60),
                new Student("Ela", 'A', 40),
                new Student("Franek", 'B', 50)
            };

            IEnumerable<IGrouping<char, Student>> teams = from s in students
                                                          group s by s.Team;

            foreach (var item in teams)
            {
                Console.WriteLine($"{item.Key} {string.Join('\n', item)}");
            }

            Console.WriteLine(string.Join(", ",
                                    from s in students
                                    group s by s.Team into team
                                    select (team.Key, team.Count())));

            string sentence = "ala ma kota ala lubi koty tomek lubi psy";
            // wykonaj zestawienie ile razy każdy z wyrazów występuje w sentence
            Console.WriteLine(string.Join(", ",
                                    from w in sentence.Split(" ")
                                    group w by w into word
                                    select (word.Key, word.Count())));

            IEnumerable<int> enumerable = ints
                .Where(n => n % 2 == 1)
                .OrderBy(x => x)
                .Select(y => y*y);
            Console.WriteLine(string.Join(", ", enumerable));

            students
                .GroupBy((student) => student.Team)
                .Select((team) => (team.Key, team.Count()));

            IOrderedEnumerable<Student> sorted = students.OrderBy(student => student.Name).ThenByDescending(student => student.Ects);
            Console.WriteLine(string.Join(", ", sorted));
            //posortuj łańcuchy w strings wg długości, a łańcuchy o tej samej długości alfabetycznie
            Console.WriteLine(string.Join(", ", strings.OrderBy(s => s.Length).ThenBy(s => s)));


            //Tworzy arr { 0, 1, 2, 3, 4 ..., 9 }
            int sum = Enumerable
                .Range(0, 10)
                .Where(n => n % 2 == 0)
                .Sum();

            int sumOdd = Enumerable
                .Range(0, 10)
                .Where(n => n % 2 == 1)
                .Select(n => n*n)
                .Sum();

            Console.WriteLine(string.Join(", ", Enumerable
                .Range(0, 10)));

            //wybiera studenta na pozycji 2 pod względem wyniku ects, albo wartość domyślna dla referencji (czyli null)
            Student second = students.OrderByDescending(s => s.Ects).ElementAtOrDefault(1);
            Console.WriteLine(second.Name);

            Student firstOfName = students.FirstOrDefault(a => a.Name.StartsWith('A'));

            Random random = new Random();
            random.Next(5);

            //wygeneruj nową tablicę 100 liczb losowych od 0 do 9
            int[] randList = Enumerable
                .Range(0, 101)
                .Select(n => random.Next(9)).ToArray();

            Console.WriteLine(string.Join(", ", randList));
        }
    }
}
