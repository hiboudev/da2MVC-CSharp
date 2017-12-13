using da2mvc.framework.model;
using System.ComponentModel;

namespace da2mvc.framework.collection.view
{
    public interface ICollectionView<ModelType> : IComponent where ModelType : IModel
    {
        void Add(ModelType[] models);
        void Remove(ModelType[] models);
        void Clear();
    }
}
