using da2mvc.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2MVC.events
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
