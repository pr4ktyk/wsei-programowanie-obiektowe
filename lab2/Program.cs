using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23, };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8 };
            shop[2] = new Computer() { Price = 5000m, Vat = 23 };
            shop[3] = new Butter();

            decimal sumvat = 0;
            decimal sumprice = 0;

            foreach (var item in shop)
            {
                sumvat += item.GetTax();
                sumprice += item.Price;
                if (item is Computer) Console.WriteLine("komputer");
                if (item is Paint) Console.WriteLine("farba");
                if (item is Butter) Console.WriteLine("masło");

                //stary sposob sprawdzania
                if (item is Computer)
                {
                    Computer comp = item as Computer;
                }

                //nowy sposob
                Computer computer = item as Computer;
                Console.WriteLine(computer?.Vat);

            }

            Console.WriteLine($"{sumvat} {sumprice}");

            IFly[] flyingObjects = new IFly[2];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Hydroplane();

            ISwim[] swimmingObjects = new ISwim[2];
            swimmingObjects[0] = (ISwim) flyingObjects[0];

            IAggregate aggregate;
            IIterator iterator = aggregate.CreateIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
        }

        abstract class Product
        {
            public virtual decimal Price { get; init; }

            public abstract decimal GetTax();
        }

        class Computer : Product
        {
            public decimal Vat { get; init; }

            public override decimal GetTax()
            {
                return Price * Vat / 100m;
            }
        }

        class Paint : Product
        {
            public decimal Vat { get; init; }
            public decimal Capacity { get; init; }
            public decimal PriceUnit { get; init; }

            public override decimal GetTax()
            {
                return Price * Capacity * Vat / 100m;
            }

            public override decimal Price
            {
                get
                {
                    return PriceUnit * Capacity;
                }
            }
        }

        class Butter : Product
        {
            public override decimal GetTax()
            {
                return 2m;
            }
        }

        abstract class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }

        class Lecturer : Person
        {
            public string AcademicDegree { get; set; }
        }

        class Student : Person
        {
            public int StudentID { get; set; }
        }

        interface IFly
        {
            void Fly();
        }

        interface ISwim
        {
            void Swim();
        }

        abstract class Animal
        {

        }

        class Duck : Animal, IFly, ISwim
        {
            public void Fly()
            {
                throw new NotImplementedException();
            }

            public void Swim()
            {
                throw new NotImplementedException();
            }
        }

        class Hydroplane : IFly, ISwim
        {
            public void Fly()
            {
                throw new NotImplementedException();
            }

            public void Swim()
            {
                throw new NotImplementedException();
            }
        }

        interface IAggregate
        {
            IIterator CreateIterator();
        }

        interface IIterator
        {
            bool HasNext();
            int GetNext();
        }

        // TODO: cw1, cw2
    }
}
