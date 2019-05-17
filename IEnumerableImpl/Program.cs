using System;
using System.Linq;
using System.Collections;

namespace IEnumerableImpl
{
    class Program
    {
        static void Main(string[] args)
        {
            var enumerate = new CustomEnumerable(0, 5);

            Console.WriteLine("You can enumerate with a normal foreach()");
            foreach (var item in enumerate)
                Console.WriteLine("Item: {0}", item);

            Console.WriteLine("You can also use LINQ operators against your enumerable.");
            foreach (var item in enumerate.Take(2))
                Console.WriteLine("Item: {0}", item);

            Console.WriteLine("Another LINQ example using a Where() method");
            foreach (var item in enumerate.Where(p => p > 4))
                Console.WriteLine("Item: {0}", item);

            Console.WriteLine("You can also use the LINQ query syntax.");
            var nums = from num in enumerate
                       where num > 2
                       select num;
            foreach (var item in nums)
                Console.WriteLine("Item: {0}", item);
        }
    }
}
