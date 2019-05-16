using System;
using System.Threading;

namespace ThreadPools
{
    class Program
    {
        static void Main(string[] args)
        {
            // Last thread.  Demonstrates using a reset event.
            var state = new WorkerState()
            {
                ResetEvent = new ManualResetEvent(false),
                State = "Hello, World!"
            };
            ThreadPool.QueueUserWorkItem(Callback, state);
            WaitHandle.WaitAll(new WaitHandle[] { state.ResetEvent });
        }

        static void Callback(object state)
        {
            WorkerState data = (WorkerState)state;
            Console.WriteLine(data?.State);
            data.ResetEvent.Set();
        }
    }

    public class WorkerState
    {
        public ManualResetEvent ResetEvent { get; set; }

        public object State { get; set; }
    }
}
