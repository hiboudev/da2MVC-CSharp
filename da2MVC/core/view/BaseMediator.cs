using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace da2mvc.core.view
{
    public class BaseMediator<ViewType>:IMediator where ViewType : IComponent
    {
        private IComponent view;
        public ViewType View { get => (ViewType)view; }
        public Dictionary<Type, MediatorEventMapping> Listeners { get; } = new Dictionary<Type, MediatorEventMapping>();

        virtual protected void ViewInitialized() { }

        public void InitializeView(IComponent view)
        {
            this.view = view;
            ViewInitialized();
        }

        public void RegisterEventListener(Type sender, string eventName, EventListener listener)
        {
            if(!Listeners.ContainsKey(sender))
                Listeners[sender] = new MediatorEventMapping(sender);

            Listeners[sender].MapEvent(eventName, listener);
        }

        //public void HandleEvent(Type sender, BaseEventArgs args)
        //{
        //    if (!Listeners.ContainsKey(sender) && !Listeners[sender].HasListener(args.EventName)) return;

        //    EventListener listener = Listeners[sender].GetListener(args.EventName);
        //    listener(args);
        //}
    }
}
