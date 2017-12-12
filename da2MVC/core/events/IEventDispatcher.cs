using System;

namespace da2mvc.core.events
{
    public interface IEventDispatcher
    {
        event EventHandler MvcEventHandler;

        void DispatchEvent(BaseEventArgs args);
    }
}
