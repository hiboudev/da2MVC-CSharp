using da2mvc.core.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.framework.menubutton.events
{
    public class MenuButtonEventArgs : BaseEventArgs
    {
        public MenuButtonEventArgs(int eventId, int itemId, string itemName) : base(eventId)
        {
            ItemId = itemId;
            ItemName = itemName;
        }

        public int ItemId { get; }
        public string ItemName { get; }
    }
}
