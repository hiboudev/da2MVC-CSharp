using da2mvc.core.events;
using da2MVC_example.events;

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
