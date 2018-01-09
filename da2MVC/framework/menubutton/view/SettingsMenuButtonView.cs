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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
            ContentTemplate = new ButtonContentTemplate();
            HorizontalContentAlignment = HorizontalAlignment.Stretch;

            ContextMenuService.SetIsEnabled(this, false);

            Content = Title;
            //Width = 140;

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
            {
                if (item is MenuItemSeparator)
                    ContextMenu.Items.Add(new Separator());
                else
                    ContextMenu.Items.Add(item);
            }
        }
    }

    // To be able to add a separator in the MenuItem list from sub-classes.
    public class MenuItemSeparator : MenuItem { }

    class ButtonContentTemplate : DataTemplate
    {
        public ButtonContentTemplate()
        {
            var panel = new FrameworkElementFactory(typeof(DockPanel));

            var text = new FrameworkElementFactory(typeof(TextBlock));
            text.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            text.SetBinding(TextBlock.TextProperty, new Binding());

            var icon = new FrameworkElementFactory(typeof(MenuButtonIcon));
            icon.SetValue(FrameworkElement.WidthProperty, 8.0);
            icon.SetValue(FrameworkElement.HeightProperty, 6.0);
            icon.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            icon.SetValue(DockPanel.DockProperty, Dock.Right);

            panel.AppendChild(icon);
            panel.AppendChild(text);

            VisualTree = panel;
        }
    }


    class MenuButtonIcon : FrameworkElement
    {
        public MenuButtonIcon()
        {
            MinWidth = MinHeight = 5;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            StreamGeometry streamGeometry = new StreamGeometry();
            using (StreamGeometryContext geometryContext = streamGeometry.Open())
            {
                geometryContext.BeginFigure(new Point(), true, true);
                PointCollection points = new PointCollection
                                             {
                                                 new Point(RenderSize.Width, 0),
                                                 new Point(RenderSize.Width / 2, RenderSize.Height)
                                             };
                geometryContext.PolyLineTo(points, true, true);
            }

            drawingContext.DrawGeometry(IsEnabled ? new SolidColorBrush(Color.FromArgb(255, 70, 70, 70)) : Brushes.DarkGray, null, streamGeometry);

        }
    }
}