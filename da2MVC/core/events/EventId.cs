using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.core.events
{
    public class EventId
    {
        private static int idCounter = 0;

        public static int New()
        {
            return ++idCounter;
        }
    }
}
