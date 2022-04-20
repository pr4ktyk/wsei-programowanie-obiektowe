/*using System;
using System.Collections;
using System.Collections.Generic;

namespace lab5
{
    class Lekcja
    {
        static void Main(string[] args)
        {
            Sandwitch sandwitch = new Sandwitch()
            {
                Bread = new Ingredient { Calories = 100, Name = "Bułka krakowska" },
                Butter = new Ingredient { Calories = 50, Name = "Delma" },
                Ham = new Ingredient { Calories = 400, Name = "Z kotła"},
                Salad = new Ingredient { Calories = 10, Name = "Lodowa"}
            };

            IEnumerator<Ingredient> enumerator = sandwitch.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            foreach (var item in sandwitch)
            {
                Console.WriteLine(item);
            }

            Parking parking = new Parking();
            foreach (var item in parking)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(string.Join(", ", parking));
            Console.WriteLine(string.Join(", ", sandwitch));

            Console.WriteLine(parking['B']);
            parking['A'] = "KT2222";
            Console.WriteLine(string.Join(", ", parking));
        }
    }

    class Sandwitch: IEnumerable<Ingredient>
    {
        public Ingredient Bread { get; init; }
        public Ingredient Butter { get; init; }
        public Ingredient Salad { get; init; }
        public Ingredient Ham { get; init; }

        public IEnumerator<Ingredient> GetEnumerator()
        {
            yield return Bread;
            yield return Butter;
            yield return Salad;
            yield return Ham;
            //return new SandwitchEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    record Ingredient
    {
        public double Calories { get; init; }
        public string Name { get; init; }
    }

    class SandwitchEnumerator : IEnumerator<Ingredient>
    {
        private Sandwitch _sandwitch;
        int counter = -1;

        public SandwitchEnumerator(Sandwitch sandwitch)
        {
            _sandwitch = sandwitch;
        }

        public Ingredient Current
        {
            get
            {
                return counter switch
                {
                    0 => _sandwitch.Bread,
                    1 => _sandwitch.Butter,
                    2 => _sandwitch.Ham,
                    3 => _sandwitch.Salad,
                    _ => null
                };
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return ++counter < 3;
        }

        public void Reset()
        {
            
        }
    }

    class Parking: IEnumerable<string>
    {
        private String[] _arr = { null, "GL678", null, null, "TK32434", "AFB4234", null };
        public string this[char slot]
        {
            get
            {
                return _arr[slot - 'A'];
            }

            set
            {
                _arr[slot - 'A'] = value;
            }

        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(string car in _arr)
            {
                if (car != null) yield return car;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
*/