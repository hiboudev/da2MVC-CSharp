using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.core.view
{
    public interface IView
    {
        event EventHandler Disposed;
        void Dispose();
    }
}
