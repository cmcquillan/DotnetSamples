// .NET best practice is to create a class derived from EventArgs
using System;

namespace BasicEvents
{
    public class NormalEventArgs : EventArgs
    {
        public int State { get; set; }

        public override string ToString() => $"{State}";
    }

    // This class serves as our container for the event.
    public class ClassWithNormalEvent
    {
        // Best practice is to use EventHandler with your custom event arguments class.
        // This attaches 'object sender' to the delegate signature.
        public event EventHandler<NormalEventArgs> RaiseNormalEvent;

        public void DoSomethingNormal(int data)
        {
            Console.WriteLine($"Doing something normal: {data}");
            OnRaiseNormalEvent(new NormalEventArgs() { State = data });
            Console.WriteLine($"Done with something normal: {data}");
        }

        protected virtual void OnRaiseNormalEvent(NormalEventArgs data)
        {
            EventHandler<NormalEventArgs> handler = this.RaiseNormalEvent;

            // A null check on the handler is necessary in case nothing has been added.
            if (handler != null)
            {
                Console.WriteLine($"Invoking NormalEvent: {data}");
                handler(this, data);
            }
            else
            {
                Console.WriteLine($"No Events to Raise: {data}");
            }
        }
    }
}