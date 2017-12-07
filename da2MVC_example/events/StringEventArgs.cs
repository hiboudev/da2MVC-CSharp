using da2mvc.events;
using da2MVC_example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
