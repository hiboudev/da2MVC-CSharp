using System;
using System.Collections.Generic;

namespace da2mvc.core.injection
{
    class CommandMapping
    {
        private Dictionary<int, List<Type>> eventMappings = new Dictionary<int, List<Type>>();

        public CommandMapping(Type dispatcherType)
        {
            DispatcherType = dispatcherType;
        }

        public void MapEvent(int eventId, Type commandType)
        {
            if (!eventMappings.ContainsKey(eventId))
                eventMappings.Add(eventId, new List<Type>());

            eventMappings[eventId].Add(commandType);
        }

        public List<Type> GetCommandTypes(int eventId)
        {
            return eventMappings[eventId];
        }

        public bool HasMapping(int eventId)
        {
            return eventMappings.ContainsKey(eventId);
        }

        public Type DispatcherType { get; }
    }
}
