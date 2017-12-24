using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace da2mvc.core.view
{
    public delegate void EventListener<EventArgsType>(EventArgsType args) where EventArgsType : BaseEventArgs;

    public class BaseMediator<ViewType>:IMediator where ViewType : IView
    {
        private IView view;
        public ViewType View { get => (ViewType)view; }
        public Dictionary<Type, MediatorEventMapping> Listeners { get; } = new Dictionary<Type, MediatorEventMapping>();

        virtual protected void ViewInitialized() { }

        public void InitializeView(IView view)
        {
            if (!(view is ViewType)) throw new Exception($"View must be of type {typeof(ViewType)}.");

            this.view = view;
            ViewInitialized();
        }

        public void HandleEvent<DispatcherType, EventArgsType>(int eventId, EventListener<EventArgsType> listener) where EventArgsType : BaseEventArgs where DispatcherType : IEventDispatcher
        {
            Type dispatcherType = typeof(DispatcherType);

            if(!Listeners.ContainsKey(dispatcherType))
                Listeners[dispatcherType] = new MediatorEventMapping(dispatcherType);
            
            Listeners[dispatcherType].MapEvent(eventId, new MediatorListenerWrapper(listener, this));
        }
    }
}
