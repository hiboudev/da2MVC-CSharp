using da2mvc.core.events;
using da2mvc.core.injection;
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
    public class CollectionMediator<CollectionType, ModelType, ViewType> : BaseMediator<ViewType> where CollectionType : CollectionModel<ModelType> where ModelType :IModel where ViewType : ICollectionView<ModelType>
    {
        public CollectionMediator()
        {
            RegisterEventListener<CollectionType, CollectionEventArgs<ModelType>>(CollectionModel<ModelType>.EVENT_ITEMS_ADDED, OnItemsAdded);
            RegisterEventListener<CollectionType, CollectionEventArgs<ModelType>>(CollectionModel<ModelType>.EVENT_ITEMS_REMOVED, OnItemsRemoved);
            RegisterEventListener<CollectionType, CollectionEventArgs<ModelType>>(CollectionModel<ModelType>.EVENT_CLEARED, OnItemsCleared);
        }

        protected override void ViewInitialized()
        {
            base.ViewInitialized();

            // To avoid forcing sub-classes to declare a ctor parameter.
            var collection = Injector.GetInstance<CollectionType>();
            if(collection.Items.Count > 0)
                View.Add(collection.Items.ToArray());
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
