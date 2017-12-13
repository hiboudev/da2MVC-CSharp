using System;

namespace da2mvc.core.events
{
    public class BaseEventArgs: EventArgs
    {
        public BaseEventArgs(int eventId)
        {
            EventId = eventId;
        }

        public int EventId { get; }
    }
}
