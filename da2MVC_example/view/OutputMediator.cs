using da2MVC_example.model;
using da2MVC_example.events;
using da2mvc.core.view;
using da2mvc.core.events;

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
