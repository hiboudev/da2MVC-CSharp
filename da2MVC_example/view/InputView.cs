using da2mvc.events;
using da2MVC_example.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace da2MVC_example.view
{
    class InputView : FlowLayoutPanel, IEventDispatcher
    {
        public event EventHandler MvcEventHandler;
        public const string EVENT_UPDATE_REQUESTED = "updateRequested";

        public InputView()
        {
            InitializeUI();
        }


        private void InitializeUI()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            TextBox inputField = new TextBox()
            {
            };

            Button okButton = new Button()
            {
                Text = "OK",
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom,
            };

            okButton.Click += (sender, args) => DispatchEvent(new StringEventArgs(EVENT_UPDATE_REQUESTED, inputField.Text));

            Controls.Add(inputField);
            Controls.Add(okButton);
        }

        public void DispatchEvent(BaseEventArgs args)
        {
            MvcEventHandler?.Invoke(this, args);
        }
    }
}
