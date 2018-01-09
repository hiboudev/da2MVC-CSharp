using da2mvc.core.events;
using da2mvc.framework.collection.events;
using da2mvc.framework.model;
using System.Collections.Generic;
using System.Linq;

namespace da2mvc.framework.collection.model
{
    public class CollectionModel<ModelType> : EventDispatcher where ModelType : IModel
    {
        public static readonly int EVENT_ITEMS_ADDED = EventId.New();
        public static readonly int EVENT_ITEMS_REMOVED = EventId.New();
        public static readonly int EVENT_CLEARED = EventId.New();

        private List<ModelType> items = new List<ModelType>();
        public List<ModelType> Items { get => items.ToList(); }

        virtual public void Add(ModelType model)
        {
            items.Add(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, new ModelType[] { model }));
        }

        virtual public void AddRange(List<ModelType> models)
        {
            items.AddRange(models);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, models.ToArray()));
        }

        virtual public void Remove(ModelType model)
        {
            items.Remove(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_REMOVED, this, new ModelType[] { model }));
        }

        virtual public void Clear()
        {
            var itemsCopy = items.ToArray();
            items.Clear();
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_CLEARED, this, itemsCopy));
        }

        public ModelType Get(int id)
        {
            foreach (ModelType model in items)
                if (model.Id == id)
                    return model;
            return default(ModelType);
        }
    }
}
