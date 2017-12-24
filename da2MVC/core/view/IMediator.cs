using da2mvc.core.events;
using da2mvc.core.injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace da2mvc.core.view
{

    public interface IMediator 
    {
        void InitializeView(IView view);
        Dictionary<Type, MediatorEventMapping> Listeners { get; }
    }
}
