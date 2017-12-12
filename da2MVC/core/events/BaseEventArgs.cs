using System;

namespace da2mvc.core.events
{
    public class BaseEventArgs: EventArgs
    {
        public BaseEventArgs(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; }
    }
}
