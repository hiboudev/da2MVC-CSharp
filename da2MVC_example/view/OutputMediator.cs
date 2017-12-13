using da2MVC_example.model;
using da2MVC_example.events;
using da2mvc.core.view;
using da2mvc.core.events;

namespace da2MVC_example.view
{
    class OutputMediator : BaseMediator<OutputView>
    {
        private DataModel model;

        public OutputMediator(DataModel model)
        {
            this.model = model;
            RegisterEventListener(typeof(DataModel), DataModel.EVENT_DATA_CHANGED, OnDataChanged);
        }

        private void OnDataChanged(BaseEventArgs args)
        {
            View.SetData(((StringEventArgs)args).Data);
        }

        protected override void ViewInitialized()
        {
            View.SetData(model.Data);
            model = null;
        }
    }
}
