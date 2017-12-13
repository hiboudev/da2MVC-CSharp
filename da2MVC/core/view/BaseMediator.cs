using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace da2mvc.core.view
{
    public delegate void EventListener<EventArgsType>(EventArgsType args) where EventArgsType : BaseEventArgs;

    public class BaseMediator<ViewType>:IMediator where ViewType : IComponent
    {
        private IComponent view;
        public ViewType View { get => (ViewType)view; }
        public Dictionary<Type, MediatorEventMapping> Listeners { get; } = new Dictionary<Type, MediatorEventMapping>();

        virtual protected void ViewInitialized() { }

        public void InitializeView(IComponent view)
        {
            if (!(view is ViewType)) throw new Exception($"View must be of type {typeof(ViewType)}.");

            this.view = view;
            ViewInitialized();
        }

        public void RegisterEventListener<EventArgsType>(Type sender, string eventName, EventListener<EventArgsType> listener) where EventArgsType : BaseEventArgs
        {
            if(!Listeners.ContainsKey(sender))
                Listeners[sender] = new MediatorEventMapping(sender);
            
            Listeners[sender].MapEvent(eventName, new MediatorListenerWrapper(listener, this));
        }
    }
}
