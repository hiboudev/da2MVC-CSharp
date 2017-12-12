using da2mvc.framework.model.events;
using da2mvc.core.events;
using System.Collections.Generic;

namespace da2mvc.framework.model
{
    public class CollectionModel<ModelType> : EventDispatcher where ModelType : IModel
    {
        public const string EVENT_ITEMS_ADDED = "itemsAdded";
        public const string EVENT_ITEMS_REMOVED = "itemsRemoved";

        public List<ModelType> Items { get; } = new List<ModelType>();

        public void Add(ModelType model)
        {
            Items.Add(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, new ModelType[] { model }));
        }

        public void AddRange(List<ModelType> models)
        {
            Items.AddRange(models);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, models.ToArray()));
        }
        
        public void Remove(ModelType model)
        {
            Items.Remove(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_REMOVED, this, new ModelType[] { model }));
        }

        public ModelType Get(int id)
        {
            foreach (ModelType model in Items)
                if (model.Id == id)
                    return model;
            return default(ModelType);
        }
    }
}
