using da2mvc.core.events;
using da2mvc.core.view;
using da2mvc.framework.model;
using da2mvc.framework.model.events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.framework.view
{
    public class CollectionMediator<CollectionType, ModelType> : BaseMediator<ICollectionView<ModelType>> where CollectionType : CollectionModel<ModelType> where ModelType :IModel
    {
        public CollectionMediator()
        {
            RegisterEventListener<CollectionEventArgs<ModelType>>(typeof(CollectionType), CollectionModel<ModelType>.EVENT_ITEMS_ADDED, OnItemsAdded);
            RegisterEventListener<CollectionEventArgs<ModelType>>(typeof(CollectionType), CollectionModel<ModelType>.EVENT_ITEMS_REMOVED, OnItemsRemoved);
            RegisterEventListener<CollectionEventArgs<ModelType>>(typeof(CollectionType), CollectionModel<ModelType>.EVENT_CLEARED, OnItemsCleared);
        }

        private void OnItemsAdded(CollectionEventArgs<ModelType> args)
        {
            View.Add(args.ChangedItems);
        }

        private void OnItemsRemoved(CollectionEventArgs<ModelType> args)
        {
            View.Remove(args.ChangedItems);
        }

        private void OnItemsCleared(CollectionEventArgs<ModelType> args)
        {
            View.Clear();
        }
    }
}
