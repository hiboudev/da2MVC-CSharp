using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.events
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
