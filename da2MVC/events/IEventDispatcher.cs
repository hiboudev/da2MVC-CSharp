using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.events
{
    public interface IEventDispatcher
    {
        event EventHandler MyEventHandler;

        void dispatchEvent(BaseEventArgs args);
    }
}
