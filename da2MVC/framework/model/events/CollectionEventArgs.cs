using da2mvc.core.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.framework.model.events
{
    public class CollectionEventArgs <ModelType>: BaseEventArgs where ModelType : IModel
    {
        public CollectionEventArgs(string eventName, CollectionModel<ModelType> collection, ModelType[] changedItems) : base(eventName)
        {
            Collection = collection;
            ChangedItems = changedItems;
        }

        public CollectionModel<ModelType> Collection { get; }
        public ModelType[] ChangedItems { get; }
    }
}
