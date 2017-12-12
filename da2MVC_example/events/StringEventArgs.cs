using da2mvc.core.events;

namespace da2MVC_example.events
{
    class StringEventArgs : BaseEventArgs
    {
        public StringEventArgs(string eventName, string Data) : base(eventName)
        {
            this.Data = Data;
        }

        public string Data { get; }
    }
}
