using System;

namespace da2mvc.core.events
{
    public class EventDispatcher : IEventDispatcher
    {
        public event EventHandler MvcEventHandler;

        public void DispatchEvent(BaseEventArgs args)
        {
            MvcEventHandler?.Invoke(this, args);
        }
    }
}
