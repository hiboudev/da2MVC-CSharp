using da2mvc.framework.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.framework.view
{
    public interface ICollectionView<ModelType> : IComponent where ModelType : IModel
    {
        void Add(ModelType[] models);
        void Remove(ModelType[] models);
        void Clear();
    }
}
