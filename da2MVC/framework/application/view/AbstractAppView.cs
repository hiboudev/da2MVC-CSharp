using da2mvc.core.injection;
using da2mvc.framework.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace da2mvc.framework.application.view
{
    abstract public class AbstractAppView : DockPanel
    {
        private Type viewType;

        public AbstractAppView()
        {
            DataContext = this;
        }

        public Type ViewType
        {
            get
            {
                return viewType;
            }
            set
            {
                if (viewType != null)
                    throw new Exception("View is already set.");

                viewType = value;

                if (WpfUtils.IsInDesignMode()) InitializeMappings();

                Children.Add((UIElement)Injector.GetInstance(viewType));
            }
        }

        abstract protected void InitializeMappings();
    }
}
