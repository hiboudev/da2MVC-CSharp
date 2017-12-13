using da2mvc.core.view;
using System;
using System.Collections.Generic;

namespace da2mvc.core.injection
{
    public class MediatorEventMapping
    {
        private Dictionary<int, MediatorListenerWrapper> listenerMappings = new Dictionary<int, MediatorListenerWrapper>();

        public MediatorEventMapping(Type dispatcherType)
        {
            DispatcherType = dispatcherType;
        }

        internal void MapEvent(int eventId, MediatorListenerWrapper listener)
        {
            listenerMappings.Add(eventId, listener);
        }

        internal bool HasListener(int eventId)
        {
            return listenerMappings.ContainsKey(eventId);
        }

        internal MediatorListenerWrapper GetListener(int eventId)
        {
            return listenerMappings[eventId];
        }

        internal Type DispatcherType { get; }
    }
}
