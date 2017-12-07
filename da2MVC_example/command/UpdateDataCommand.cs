using da2mvc.command;
using da2MVC_example.events;
using da2MVC_example.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2MVC_example.command
{
    class UpdateDataCommand : ICommand
    {
        private readonly StringEventArgs args;
        private readonly DataModel model;

        public UpdateDataCommand(StringEventArgs args, DataModel model)
        {
            this.args = args;
            this.model = model;
        }

        public void Execute()
        {
            model.SetData(args.Data);
        }
    }
}
