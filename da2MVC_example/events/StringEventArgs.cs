using da2mvc.core.events;

namespace da2MVC_example.events
{
    class StringEventArgs : BaseEventArgs
    {
        public StringEventArgs(int eventId, string Data) : base(eventId)
        {
            this.Data = Data;
        }

        public string Data { get; }
    }
}
