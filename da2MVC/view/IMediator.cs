using MVC_Framework.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using da2mvc.events;

namespace MVC_Framework.view
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
