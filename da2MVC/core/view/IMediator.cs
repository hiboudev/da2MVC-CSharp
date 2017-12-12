using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace da2mvc.core.view
{
    public delegate void EventListener(BaseEventArgs args);

    public interface IMediator //<ViewType> where ViewType:IComponent
    {
        void InitializeView(IComponent view);
        IComponent View { get; }
        Dictionary<Type, MediatorEventMapping> Listeners { get; }

        void RegisterEventListener(Type sender, string eventName, EventListener listener);
        void HandleEvent(Type sender, BaseEventArgs args);
    }
}
