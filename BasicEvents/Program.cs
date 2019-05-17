using System;

namespace BasicEvents
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== NORMAL EVENTS =====");
            // Create a class with an event attached.
            var item = new ClassWithNormalEvent();
            // Try some work before an event is attached
            item.DoSomethingNormal(1);

            item.RaiseNormalEvent += PerformNormalProcess;

            // Do the work which raises the event.
            item.DoSomethingNormal(5);
            item.DoSomethingNormal(10);

            // Unsubscribe the event
            item.RaiseNormalEvent -= PerformNormalProcess;
            item.DoSomethingNormal(11);

            // Subscribe with two methods
            item.RaiseNormalEvent += PerformNormalProcess;
            item.RaiseNormalEvent += AppendNormalProcess;
            item.DoSomethingNormal(20);

            Console.WriteLine("===== ACCESSORS =====");
            var weirdItem = new ClassWithWeirdEvents();
            weirdItem.RaiseWeirdEvent += PerformWeirdProcess1;
            weirdItem.RaiseWeirdEvent += PerformWeirdProcess2;
            weirdItem.DoSomethingWeird(33);
        }

        private static void PerformNormalProcess(object sender, NormalEventArgs state)
        {
            Console.WriteLine($"Performing Normal Process: {state}");
        }

        private static void AppendNormalProcess(object sender, NormalEventArgs state)
        {
            Console.WriteLine($"Processing appended to Normal Process: {state}");
        }
        
        private static void PerformWeirdProcess1(object sender, WeirdEventArgs e)
        {
            Console.WriteLine($"Performing Weird Process 1: data={e.State}; event={e.EventNumberInvoked}");
        }

        private static void PerformWeirdProcess2(object sender, WeirdEventArgs e)
        {
            Console.WriteLine($"Performing Weird Process 2: data={e.State}; event={e.EventNumberInvoked}");
        }
    }
}
