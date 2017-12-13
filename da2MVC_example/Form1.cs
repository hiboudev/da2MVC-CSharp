using da2mvc.core.injection;
using da2MVC_example.view;
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

            layout.Controls.Add(Injector.GetInstance<InputView>());
            layout.Controls.Add(Injector.GetInstance<OutputView>());

            Controls.Add(layout);
        }
    }
}
