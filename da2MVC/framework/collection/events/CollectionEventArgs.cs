using da2mvc.core.events;
using da2mvc.framework.collection.model;
using da2mvc.framework.model;

namespace da2mvc.framework.collection.events
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
