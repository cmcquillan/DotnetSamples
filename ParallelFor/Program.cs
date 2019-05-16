using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelFor
{
    class Program
    {
        public static List<string> Strings { get; } = new List<string>
        {
            "Hello",
            "World",
            "This",
            "Is",
            "A",
            "List",
            "Of",
            "Strings",
            "That",
            "Could",
            "Be",
            "Processed"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("===== TASK 1 =====");
            // Run a foreach of every item in parallel.
            Parallel.ForEach(Strings, (item, state, num) =>
            {
                Console.WriteLine($"Data ({num}): {item}");
            });


            Console.WriteLine("===== TASK 2 =====");
            // Try to stop the loop after finding a certain point.
            Parallel.ForEach(Strings, (item, state, num) =>
            {
                Console.WriteLine($"Data ({num}): {item}");

                if (item == "Processed")
                {
                    Console.WriteLine("Found the stopword.  Stopping...");
                    state.Break();
                }
            });

            Console.WriteLine("===== TASK 3 =====");
            // Artificially facilitate stopping the loop by delaying for the larger inputs.
            Parallel.ForEach(Strings, (item, state, num) =>
            {
                Thread.Sleep(10 * (int)num);

                if (state.ShouldExitCurrentIteration)
                    return;

                Console.WriteLine($"Data ({num}): {item}");

                if (item == "Hello")
                {
                    Console.WriteLine("Found the stopword.  Stopping...");
                    state.Break();
                }
            });

            Console.WriteLine("===== TASK 4 =====");
            // Facilitate using Parallel.For
            Parallel.For(0, Strings.Count, (num, state) =>
            {
                Console.WriteLine($"Data ({num}): {Strings[num]}");
            });

            Console.WriteLine("===== TASK 5 =====");
            // Change degree of parallelism.  Note that they process in order now.
            Parallel.For(0, Strings.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }, (num, state) =>
            {
                Console.WriteLine($"Data ({num}): {Strings[num]}");
            });
        }
    }
}
