using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace da2mvc.framework.application.view
{
    public class ModalWindow : Window
    {
        public ModalWindow()
        {
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            Background = new SolidColorBrush(Colors.WhiteSmoke);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            InitializeContent((FrameworkElement)newContent);
        }

        virtual protected void InitializeContent(FrameworkElement content)
        {
            content.Margin = new Thickness(4);
            //(newContent as Panel).Background = new SolidColorBrush(Colors.White);
        }
    }
}
