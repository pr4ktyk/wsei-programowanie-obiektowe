using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23, };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8};
            shop[2] = new Computer() { Price = 5000m, Vat = 23 };
            shop[1] = new Paint() { PriceUnit = 7, Capacity = 10, Vat = 8 };

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
                return Price * Vat/100m;
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
                    return Price * Vat / 100m;
                }
            }
        }
    }
}
