using da2mvc.framework.model.events;
using da2mvc.core.events;
using System.Collections.Generic;

namespace da2mvc.framework.model
{
    public class CollectionModel<ModelType> : EventDispatcher where ModelType : IModel
    {
        public const string EVENT_ITEMS_ADDED = "itemsAdded";
        public const string EVENT_ITEMS_REMOVED = "itemsRemoved";

        private List<ModelType> Collection { get; } = new List<ModelType>();

        public void Add(ModelType model)
        {
            Collection.Add(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, new ModelType[] { model }));
        }

        public void AddRange(List<ModelType> models)
        {
            Collection.AddRange(models);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, models.ToArray()));
        }
        
        public void Remove(ModelType model)
        {
            Collection.Remove(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_REMOVED, this, new ModelType[] { model }));
        }

        public ModelType Get(int id)
        {
            foreach (ModelType model in Collection)
                if (model.Id == id)
                    return model;
            return default(ModelType);
        }
    }
}
