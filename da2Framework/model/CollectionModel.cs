using da2MVC.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2Framework.model
{
    public class CollectionModel<ModelType> : EventDispatcher where ModelType : IModel
    {
        private List<ModelType> Collection { get; } = new List<ModelType>();

        public void Add(ModelType model)
        {
            Collection.Add(model);
        }

        public void AddRange(List<ModelType> models)
        {
            Collection.AddRange(models);
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
