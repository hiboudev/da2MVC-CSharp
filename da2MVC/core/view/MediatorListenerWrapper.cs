using da2mvc.core.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.core.view
{
    class MediatorListenerWrapper
    {
        private readonly Delegate listener;
        private readonly IMediator mediator;

        public MediatorListenerWrapper(Delegate listener, IMediator mediator)
        {
            this.listener = listener;
            this.mediator = mediator;
        }

        public void Invoke(BaseEventArgs args)
        {
            listener.Method.Invoke(mediator, new object[] { args });
        }
    }
}
