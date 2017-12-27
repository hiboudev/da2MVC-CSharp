using da2mvc.core.events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace da2mvc.framework.menubutton.view
{
    public class SettingsMenuButtonView : Button, IEventDispatcher
    {
        protected List<MenuItem> builtInItems;
        public event EventHandler MvcEventHandler;
        protected bool DrawArrow { get; set; } = true;
        // Concerns only the second part of title
        public int TitleMaxLength { get; protected set; } = 18;


        public SettingsMenuButtonView()
        {
            InitializeUI();
        }

        virtual protected string Title => "Menu";

        virtual protected void Redraw()
        {
            ContextMenu.Items.Clear();
            AddRange(builtInItems.ToArray());
        }

        private void InitializeUI()
        {
            ContextMenuService.SetIsEnabled(this, false);

            Content = Title;
            //Width = 140;
            HorizontalContentAlignment = HorizontalAlignment.Center;

            // To avoid a bug when there's several instances, opening one will push focus to the next one, cause of the Enabled=false.
            //SetStyle(ControlStyles.Selectable, false); // TODO WPF?

            // I can't find how to disable word wrapping in Button. If text is in one word and too long, it displays nothing.
            //AutoSize = true; // TODO WPF?

            ContextMenu = new ContextMenu();
            ContextMenu.Opened += MenuOpening;
            ContextMenu.Closed += MenuClosed;

            builtInItems = GetBuiltInItems();

            Click += OnButtonClick;

            Redraw();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (ContextMenu.Items.Count > 0)
            {
                ContextMenu.Placement = PlacementMode.Relative;
                ContextMenu.PlacementRectangle = new Rect(PointToScreen(new Point(0, ActualHeight)), new Size());
                ContextMenu.IsOpen = true;
            }
        }

        virtual protected List<MenuItem> GetBuiltInItems()
        {
            return new List<MenuItem>();
        }

        public void DispatchEvent(BaseEventArgs args)
        {
            MvcEventHandler?.Invoke(this, args);
        }

        private void MenuClosed(object sender, RoutedEventArgs e)
        {
            IsEnabled = true;
        }

        private void MenuOpening(object sender, EventArgs e)
        {
            IsEnabled = false;
        }

        protected void AddRange(MenuItem[] items)
        {
            foreach (var item in items)
                ContextMenu.Items.Add(item);
        }
    }
}
