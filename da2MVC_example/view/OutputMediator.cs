using da2mvc.view;
using da2MVC_example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using da2mvc.events;
using da2MVC_example.events;

namespace da2MVC_example.view
{
    class OutputMediator : BaseMediator
    {
        private DataModel model;

        public OutputMediator(DataModel model)
        {
            RegisterEventListener(typeof(DataModel), DataModel.EVENT_DATA_CHANGED, OnDataChanged);
            this.model = model;
        }

        private void OnDataChanged(BaseEventArgs args)
        {
            ((OutputView)View).SetData(((StringEventArgs)args).Data);
        }

        protected override void ViewInitialized()
        {
            ((OutputView)View).SetData(model.Data);
            model = null;
        }
    }
}
