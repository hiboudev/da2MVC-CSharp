using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace da2mvc.core.view
{

    public interface IMediator 
    {
        void InitializeView(IComponent view);
        Dictionary<Type, MediatorEventMapping> Listeners { get; }
    }
}
