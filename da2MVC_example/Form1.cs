using da2mvc.injection;
using da2MVC_example.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace da2MVC_example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Mappings.Initialize();

            InitializeUI();
        }

        private void InitializeUI()
        {
            var layout = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
            };

            var inputView = (InputView)Injector.GetInstance(typeof(InputView));
            var outputView = (OutputView)Injector.GetInstance(typeof(OutputView));

            layout.Controls.Add(inputView);
            layout.Controls.Add(outputView);

            Controls.Add(layout);
        }
    }
}
