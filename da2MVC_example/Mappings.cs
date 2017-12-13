using da2mvc.core.injection;
using da2MVC_example.command;
using da2MVC_example.model;
using da2MVC_example.view;

namespace da2MVC_example
{
    class Mappings
    {
        public static void Initialize()
        {
            Injector.MapType<DataModel>(true);
            Injector.MapType<InputView>(true);

            Injector.MapView<OutputView, OutputMediator>(true);

            Injector.MapCommand<InputView, UpdateDataCommand>(InputView.EVENT_UPDATE_REQUESTED);
        }
    }
}
