using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace da2MVC_example.view
{
    class OutputView:Panel
    {
        private Label label;

        public OutputView()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Size = new Size(100, 20);
            label = new Label()
            {
                BackColor = Color.LightGray,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            Controls.Add(label);
        }

        internal void SetData(string data)
        {
            label.Text = data;
        }
    }
}
