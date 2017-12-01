using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.injection
{
    class CommandMapping
    {
        private Dictionary<string, List<Type>> eventMappings = new Dictionary<string, List<Type>>();

        public CommandMapping(Type dispatcherType)
        {
            DispatcherType = dispatcherType;
        }

        public void MapEvent(string eventName, Type commandType)
        {
            if (!eventMappings.ContainsKey(eventName))
                eventMappings.Add(eventName, new List<Type>());

            eventMappings[eventName].Add(commandType);
        }

        public List<Type> GetCommandTypes(string eventName)
        {
            return eventMappings[eventName];
        }

        public bool HasMapping(string eventName)
        {
            return eventMappings.ContainsKey(eventName);
        }

        public Type DispatcherType { get; }
    }
}
