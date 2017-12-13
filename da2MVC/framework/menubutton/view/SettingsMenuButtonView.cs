using da2mvc.core.events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace da2mvc.framework.menubutton.view
{
    public class SettingsMenuButtonView : Button, IEventDispatcher
    {
        protected List<ToolStripMenuItem> builtInItems;
        public event EventHandler MvcEventHandler;
        protected bool DrawArrow { get; set; } = true;
        public int TitleMaxLength { get; protected set; } = 18;


        public SettingsMenuButtonView()
        {
            InitializeUI();
        }

        virtual protected string Title => "Menu";

        virtual protected void Redraw()
        {
            ContextMenuStrip.Items.Clear();
            ContextMenuStrip.Items.AddRange(builtInItems.ToArray());
        }

        private void InitializeUI()
        {
            BackColor = Color.LightGray;
            Text = Title;
            Width = 140;
            TextAlign = ContentAlignment.MiddleLeft;
            // To avoid a bug when there's several instances, opening one will push focus to the next one, cause of the Enabled=false.
            SetStyle(ControlStyles.Selectable, false);
            // I can't find how to disable word wrapping in Button. If text is in one word and too long, it displays nothing.
            AutoSize = true;

            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Opening += MenuOpening;
            ContextMenuStrip.Closed += MenuClosed;

            builtInItems = GetBuiltInItems();
            //foreach (ToolStripMenuItem item in builtInItems)
            //    item.Font = new Font(item.Font, FontStyle.Italic);

            Redraw();
        }

        virtual protected List<ToolStripMenuItem> GetBuiltInItems()
        {
            return new List<ToolStripMenuItem>();
        }

        public void DispatchEvent(BaseEventArgs args)
        {
            MvcEventHandler?.Invoke(this, args);
        }

        private void MenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Enabled = true;
        }

        private void MenuOpening(object sender, EventArgs e)
        {
            Enabled = false;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (ContextMenuStrip.Items.Count > 0)
                ContextMenuStrip.Show(this, new Point(Location.X, Height));
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (DrawArrow && ContextMenuStrip != null)
            {
                int arrowX = ClientRectangle.Width - 14;
                int arrowY = ClientRectangle.Height / 2 - 1;

                Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
                Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
                pevent.Graphics.FillPolygon(brush, arrows);
            }
        }
    }
}
