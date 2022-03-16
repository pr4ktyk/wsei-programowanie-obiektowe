using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Money.Of(-12, Currency.PLN) == null ? Money.Of(0, Currency.PLN) : Money.Of(-12, Currency.PLN);
            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);

            Money a = money * 2m;
            Money b = money * 10m;

            Console.WriteLine($"money * 2: {a.Value};\nmoney * 10: {b.Value};");
            Console.WriteLine();
            Money c = money + 1m;
            Money d = 7m + money;
            Console.WriteLine($"money + 1: {c.Value};\n7 + money: {d.Value};");
            Console.WriteLine();
            Console.WriteLine($"c < d: {c < d};\nd > c: {d > c};");

            int x = 10;
            long y = 10L;

            y = x;
            x = (int)y;

            decimal price = money;
            double cost = (double)money;

            float z = (float)money;

            Console.WriteLine($"x == y: {x == y};");
            Console.WriteLine();
            Console.WriteLine($"money.Equals(12, PLN) -> {money.Equals(Money.Of(12, Currency.PLN))}");
            Console.WriteLine($"money.Equals(13, PLN) -> {money.Equals(Money.Of(13, Currency.PLN))}");
            Console.WriteLine();
            Console.Write("money (wyswietla 12, bo jest niejawne rzutowanie) -> ");
            Console.WriteLine(money);
            Console.WriteLine($"money.ToString() -> {money}");

            Person p1 = Person.OfName("Jan");
            Console.WriteLine($"person.ToString() -> {p1}");

            Money[] prices =
            {
                Money.Of(10, Currency.PLN),
                Money.Of(5, Currency.EUR),
                Money.Of(50, Currency.USD),
                Money.Of(20, Currency.PLN)
            };

            Array.Sort(prices);

            foreach(var p in prices)
            {
                Console.WriteLine($"{p.Value} {p.Currency};");
            }
            Console.WriteLine();

            Tank zbiornik = new Tank(50);
            Console.WriteLine($"{zbiornik}");
            zbiornik.refuel(15);
            Console.WriteLine($"Tankowanie 15l\n{zbiornik}");
            Tank zbiornik2 = new Tank(10);
            Console.WriteLine($"zbiornik1: {zbiornik}; zbiornik2: {zbiornik2}");
            zbiornik2.refuel(zbiornik, 5);
            Console.WriteLine($"Zbiornik1  -- 5l --> Zbiornik2");
            Console.WriteLine($"zbiornik1: {zbiornik}; zbiornik2: {zbiornik2}");
            Console.WriteLine();
            Student[] listOfStudents =
            {
                Student.Register("Małysz", "Adam", 3),
                Student.Register("Pudzianowski", "Mariusz", 3),
                Student.Register("Józef", "Piłsudski", 3),
                Student.Register("Pudzianowski", "Krystian", 3),
                Student.Register("Małysz", "Adam", 2),
                Student.Register("Wojtyła", "Karol", 3),
            };
            Console.WriteLine("Przed sortowaniem:");
            foreach (var student in listOfStudents)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();
            Array.Sort(listOfStudents);
            Console.WriteLine("Po sortowaniu:");
            foreach (var student in listOfStudents)
            {
                Console.WriteLine(student);
            }
        }
    }

    public class Person
    {
        private string firstName;

        public static Person OfName(string name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię jest zbyt krótkie.");
            }
        }
        private Person(string _firstName)
        {
            firstName = _firstName;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię jest zbyt krótkie.");
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {firstName};";
        }
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public decimal Value
        {
            get { return _value; }
        }


        // Money * 4 --> *(money, 4)
        public static Money operator *(Money money, decimal factor)
        {
            return Money.Of(money._value * factor, money.Currency);
        }

        public static Money operator *(decimal factor, Money money)
        {
            return Money.Of(money._value * factor, money.Currency);
        }

        public static Money operator +(Money val1, Money val2)
        {
            IsSameCurrencies(val1, val2);

            return Money.Of(val1.Value + val2.Value, val1.Currency);
        }

        public static bool operator <(Money val1, Money val2)
        {
            IsSameCurrencies(val1, val2);

            return val1.Value < val2.Value;
        }

        public static bool operator >(Money val1, Money val2)
        {
            IsSameCurrencies(val1, val2);

            return val1.Value > val2.Value;
        }

        private static void IsSameCurrencies(Money val1, Money val2)
        {
            if (val1.Currency != val2.Currency)
            {
                throw new Exception();
            }
        }

        public static Money operator +(Money money, decimal factor)
        {
            return Money.Of(money._value + factor, money.Currency);
        }

        public static Money operator +(decimal factor, Money money)
        {
            return Money.Of(money._value + factor, money.Currency);
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithException(decimal value, Currency currency)
        {
            return value < 0 ? throw new ArgumentOutOfRangeException("Wartość musi być większa niż 0.") : new Money(value, currency);
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency};";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
        {
            return other != null &&
                   _value == other._value &&
                   _currency == other._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }

        public int CompareTo(Money other)
        {
            int result = _currency.CompareTo(other._currency);

            if(result == 0)
            {
                result = _value.CompareTo(other._value);
            }

            return result;
        }
    }

    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }

        public bool refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        public void refuelSecondMethod(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Argument cant be non positive!");
            }
            if (_level + amount > Capacity)
            {
                throw new ArgumentException("Argument is to large!");
            }
            _level += amount;
        }

        public bool refuel(Tank sourceTank, int amount)
        {
            if (amount <= 0) return false;
            if (sourceTank._level < amount) return false;
            if (amount + _level > Capacity) return false;

            sourceTank._level -= amount;
            _level += amount;
            return true;
        }

        public override string ToString()
        {
            return $"Tank: Capacity = {Capacity}, Level = {_level}";
        }
    }

    class Student : IComparable<Student>
    {
            private string _nazwisko;
            public string Nazwisko
            {
                get => _nazwisko;
                set
                {
                    _nazwisko = value;
                }
            }

        private string _imie;
            public string Imie
            {
                get => _imie;
                set
                {
                    _imie = value;
                }
            }

            private decimal _srednia;
            public decimal Srednia
            {
                get => _srednia;
                set
                {
                    _srednia = value;
                }
            }

        public Student(string nazwisko, string imie, decimal srednia)
        {
            _nazwisko = nazwisko;
            _imie = imie;
            _srednia = srednia;
        }

        public int CompareTo(Student other)
        {
            if (_nazwisko.CompareTo(other._nazwisko) == 0)
            {
                return (_imie.CompareTo(other._imie) == 0) ? _imie.CompareTo(other._imie) : _imie.CompareTo(other._imie);

            } else return _nazwisko.CompareTo(other._nazwisko);
        }

        public static Student? Register(string _imie, string _nazwisko, decimal _srednia)
        {
            return new Student(_imie, _nazwisko, _srednia);
        }

        public override string ToString()
        {
            return $"Surname: {_nazwisko}; Name: {_imie}; Avg: {_srednia}";
        }
    }
}