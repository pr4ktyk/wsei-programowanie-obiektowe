using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Money.Of(-12, Currency.PLN) == null ? Money.Of(0, Currency.PLN) : Money.Of(-12, Currency.PLN);
            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);

            Money result = money * 0.22m;
            Console.WriteLine(result.Value);
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
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money
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
    }
}