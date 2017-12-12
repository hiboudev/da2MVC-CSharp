using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace da2mvc.core.view
{
    public class BaseMediator:IMediator//<ViewType> : IMediator<ViewType> where ViewType : IComponent
    {
        private Dictionary<Type, MediatorEventMapping> listeners;
        public Dictionary<Type, MediatorEventMapping> Listeners { get => listeners; }

        public IComponent View { get; private set; }

        public BaseMediator()
        {
            listeners = new Dictionary<Type, MediatorEventMapping>();
        }

        public void InitializeView(IComponent view)
        {
            View = view;
            ViewInitialized();
        }

        virtual protected void ViewInitialized(){}

        public void RegisterEventListener(Type sender, string eventName, EventListener listener)
        {
            if(!listeners.ContainsKey(sender))
                listeners[sender] = new MediatorEventMapping(sender);

            listeners[sender].MapEvent(eventName, listener);
        }

        public void HandleEvent(Type sender, BaseEventArgs args)
        {
            if (!listeners.ContainsKey(sender) && !listeners[sender].HasListener(args.EventName)) return;

            EventListener listener = listeners[sender].GetListener(args.EventName);
            listener(args);
        }
    }
}
