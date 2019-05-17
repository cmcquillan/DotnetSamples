using System;

namespace BasicEvents
{
    // Same general principle Normal Event
    public class WeirdEventArgs : EventArgs
    {
        public int State { get; set; }

        public int NumEvents { get; set; }

        public int EventNumberInvoked { get; set; }
    }

    // This class will use event accessors to slightly
    // modify the behavior for each callback.
    public class ClassWithWeirdEvents
    {
        private object _lockObject = new object();
        private int _numberOfEvents = 0;
        // Private event handler to hide from outside.
        private EventHandler<WeirdEventArgs> _raiseWeirdEvent;

        // An event property with accessors to control add/remove behavior.
        // Locks are used because this operation is not thread-safe.
        public event EventHandler<WeirdEventArgs> RaiseWeirdEvent
        {
            add
            {
                lock (_lockObject)
                {
                    _raiseWeirdEvent += value;
                    _numberOfEvents++;
                }
            }
            remove
            {
                lock (_lockObject)
                {
                    _raiseWeirdEvent -= value;
                    _numberOfEvents--;
                }
            }
        }

        public void DoSomethingWeird(int data)
        {
            Console.WriteLine($"Doing something weird: {data}");
            OnRaiseWeirdEvent(data);
        }

        private void OnRaiseWeirdEvent(int data)
        {
            EventHandler<WeirdEventArgs> handler = _raiseWeirdEvent;
            int eventBeingFired = 1;

            if (handler != null)
            {
                // We iterate over each internal handler.
                // Then construct special event arguments for each call
                // Which will pass in the specific event number being
                // fired.
                //
                // Since these are invoked sequentially, it would also
                // be possible to use a single instance of WeirdEventArgs
                // and then modify EventNumberInvoked prior to each call.
                foreach (EventHandler<WeirdEventArgs> item in handler.GetInvocationList())
                {
                    var args = new WeirdEventArgs()
                    {
                        State = data,
                        NumEvents = _numberOfEvents,
                        EventNumberInvoked = eventBeingFired++,
                    };

                    Console.WriteLine($"Invoking WeirdEvent: data={data}; event={eventBeingFired}");
                    item(this, args);
                }
            }
        }
    }
}