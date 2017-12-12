using da2mvc.core.view;
using System;
using System.Collections.Generic;

namespace da2mvc.core.injection
{
    public class MediatorEventMapping
    {
        private Dictionary<string, EventListener> listenerMappings = new Dictionary<string, EventListener>();

        public MediatorEventMapping(Type dispatcherType)
        {
            DispatcherType = dispatcherType;
        }

        public void MapEvent(string eventName, EventListener listener)
        {
            listenerMappings.Add(eventName, listener);
        }

        public bool HasListener(string eventName)
        {
            return listenerMappings.ContainsKey(eventName);
        }

        public EventListener GetListener(string eventName)
        {
            return listenerMappings[eventName];
        }

        public Type DispatcherType { get; }
    }
}
