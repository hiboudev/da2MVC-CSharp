using da2mvc.injection;
using da2MVC_example.command;
using da2MVC_example.model;
using da2MVC_example.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2MVC_example
{
    class Mappings
    {
        public static void Initialize()
        {
            Injector.MapType(typeof(DataModel), true);
            Injector.MapType(typeof(InputView), true);

            Injector.MapView(typeof(OutputView), typeof(OutputMediator), true);

            Injector.MapCommand(typeof(InputView), InputView.EVENT_UPDATE_REQUESTED, typeof(UpdateDataCommand));
        }
    }
}
