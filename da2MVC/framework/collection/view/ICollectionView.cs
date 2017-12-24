using da2mvc.core.view;
using da2mvc.framework.model;
using System.ComponentModel;
using System.Windows;

namespace da2mvc.framework.collection.view
{
    public interface ICollectionView<ModelType> : IView where ModelType : IModel
    {
        void Add(ModelType[] models);
        void Remove(ModelType[] models);
        void Clear();
    }
}
