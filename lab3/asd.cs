/*using System;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> stringStack = new Stack<string>();
            stringStack.Push("abcde");

            Stack<object> objectStack = new Stack<object>();
            objectStack.Push(1);
            string v = stringStack.Pop();
            object v1 = objectStack.Pop();

            Reward reward = new Reward();
            reward.GiveTo("director");
            ValueTuple<string, decimal, int> product = ValueTuple.Create("komputer", 1200m, 2);
            Console.WriteLine(product.Item1);
            Console.WriteLine(product);

            (string, decimal, int) product2 = ("komputer", 1200m, 2);

            ( string, decimal, int )[] productList = new (string, decimal, int)[2];
            productList[0] = ("komputer", 1200m, 2);

            Console.WriteLine(product == product2 && product2 == productList[0]);

            product2 = (name: "komputer", price: 1200m, quantity: 2);
            Console.WriteLine(product2.Item1);

            (string name, decimal price, int quantity) product3 = ("komputer", 1200m, 2);
            Console.WriteLine(product3.name);

            Console.WriteLine("######");
            var test = (firstAndLast: new int[] { 2, 2 }, isSame: 2.Equals(2) ? true : false);
            Console.WriteLine(test.firstAndLast);
            Console.WriteLine(test.isSame);
        }

        public abstract class Iterator
        {
            public abstract int GetNext();

            public abstract bool HasNext();
        }


        class Stack<T>
        {
            private T[] _arr = new T[100];
            private int _last = -1;

            public void Push(T item)
            {
                // warunki testujące _last
                _arr[++_last] = item;
            }

            public T Pop()
            {
                // warunki testujące _last
                return _arr[_last--];
            }
        }

        class Reward
        {
            public Reward GiveTo<T>(T target)
            {
                Console.WriteLine($"Reward goes to: {target}");
                return this;
            }
        }
    }
}
*/