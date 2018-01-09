using da2mvc.core.events;
using da2mvc.framework.collection.view;
using da2mvc.framework.menubutton.events;
using da2mvc.framework.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace da2mvc.framework.menubutton.view
{
    public class MenuButtonView<ModelType> : SettingsMenuButtonView, ICollectionView<ModelType> where ModelType : IModel
    {
        private List<MenuItem> regularItems = new List<MenuItem>();
        public static readonly int EVENT_ITEM_CLICKED = EventId.New();

        public event EventHandler Disposed;

        public MenuButtonView()
        {
        }

        public void Add(ModelType[] models)
        {
            foreach (var model in models)
            {
                MenuItem menuItem = ModelToItem(model);

                menuItem.Click += ItemClicked;
                regularItems.Add(menuItem);
            }

            regularItems.Sort((x, y) => string.Compare(x.Header.ToString(), y.Header.ToString()));
            Redraw();
        }

        public void Remove(ModelType[] models)
        {
            foreach (var model in models)
                foreach (var item in regularItems)
                    if ((int)item.Tag == model.Id)
                    {
                        regularItems.Remove(item);
                        //DisposeItem(item); // TODO WPF?
                        break;
                    }

            regularItems.Sort((x, y) => string.Compare(x.Header.ToString(), y.Header.ToString()));
            Redraw();
        }

        public void Clear()
        {
            //foreach (var item in regularItems)
            //    DisposeItem(item); // TODO WPF?

            regularItems.Clear();
            Redraw();
        }

        virtual protected MenuItem ModelToItem(ModelType model)
        {
            return new MenuItem()
            {
                Header = model.Name,
                Tag = model.Id,
            };
        }

        /**
         * If a bitmap was created for item icon, dispose it.
         */
        //protected virtual void DisposeItem(MenuItem item)
        //{
        //    item.Dispose(); // TODO WPF?
        //}

        virtual public bool SetSelectedItem(string name)
        {
            MenuItem checkedItem = null;

            foreach (MenuItem item in regularItems)
            {
                if (item.Header.ToString() == name)
                {
                    item.IsChecked = true;
                    checkedItem = item;
                }
                else item.IsChecked = false;
            }

            if (checkedItem != null) UpdateTitle(checkedItem.Header.ToString());
            else UpdateTitle(null);

            return checkedItem != null;
        }

        virtual public bool SetSelectedItem(int id)
        {
            MenuItem checkedItem = null;

            foreach (MenuItem item in regularItems)
            {
                if ((int)item.Tag == id)
                {
                    item.IsChecked = true;
                    checkedItem = item;
                }
                else item.IsChecked = false;
            }

            if (checkedItem != null) UpdateTitle(checkedItem.Header.ToString());
            else UpdateTitle(null);

            return checkedItem != null;
        }

        private void UpdateTitle(string selectedName)
        {
            if (selectedName == null)
            {
                Content = $"{Title}";
                return;
            }

            Content = FormatTitle(selectedName);
        }

        virtual protected string FormatTitle(string selectedName)
        {
            return $"{Title}: {selectedName}";
        }

        override protected void Redraw()
        {
            ContextMenu.Items.Clear();
            AddRange(regularItems.ToArray());
            if (builtInItems.Count > 0 && regularItems.Count > 0)
                ContextMenu.Items.Add(new Separator());
            AddRange(builtInItems.ToArray());
        }

        protected void EnableItem(MenuItem item, bool enabled)
        {
            item.FontStyle = enabled ? FontStyles.Normal : FontStyles.Italic;
            item.IsEnabled = enabled;
        }

        private void ItemClicked(object sender, EventArgs e)
        {
            DispatchEvent(new MenuButtonEventArgs(EVENT_ITEM_CLICKED, (int)((MenuItem)sender).Tag, ((MenuItem)sender).Header.ToString()));
        }

        public void Dispose()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
