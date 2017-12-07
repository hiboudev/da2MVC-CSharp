
using da2MVC.events;
using da2MVC_example.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2MVC_example.model
{
    class DataModel:EventDispatcher
    {
        public const string EVENT_DATA_CHANGED = "dataChanged";

        public string Data { get; private set; } = "Default";

        public void SetData(string data)
        {
            Data = data;
            DispatchEvent(new StringEventArgs(EVENT_DATA_CHANGED, Data));
        }
    }
}
