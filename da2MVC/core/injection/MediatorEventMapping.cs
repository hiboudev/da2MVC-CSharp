using da2mvc.core.view;
using System;
using System.Collections.Generic;

namespace da2mvc.core.injection
{
    public class MediatorEventMapping
    {
        private Dictionary<string, MediatorListenerWrapper> listenerMappings = new Dictionary<string, MediatorListenerWrapper>();

        public MediatorEventMapping(Type dispatcherType)
        {
            DispatcherType = dispatcherType;
        }

        internal void MapEvent(string eventName, MediatorListenerWrapper listener)
        {
            listenerMappings.Add(eventName, listener);
        }

        internal bool HasListener(string eventName)
        {
            return listenerMappings.ContainsKey(eventName);
        }

        internal MediatorListenerWrapper GetListener(string eventName)
        {
            return listenerMappings[eventName];
        }

        internal Type DispatcherType { get; }
    }
}
