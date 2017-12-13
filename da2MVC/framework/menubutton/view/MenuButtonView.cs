using da2mvc.core.events;
using da2mvc.framework.collection.view;
using da2mvc.framework.menubutton.events;
using da2mvc.framework.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace da2mvc.framework.menubutton.view
{
    public class MenuButtonView<ModelType> : SettingsMenuButtonView, ICollectionView<ModelType> where ModelType : IModel
    {
        private List<ToolStripMenuItem> regularItems = new List<ToolStripMenuItem>();
        public const string EVENT_ITEM_CLICKED = "itemClicked";

        public MenuButtonView()
        {
        }

        public void Add(ModelType[] models)
        {
            foreach (var model in models)
            {
                ToolStripMenuItem toolItem = ModelToItem(model);

                toolItem.Click += ItemClicked;
                regularItems.Add(toolItem);
            }

            regularItems.Sort((x, y) => string.Compare(x.Text, y.Text));
            Redraw();
        }

        public void Remove(ModelType[] models)
        {
            foreach (var model in models)
                foreach (var item in regularItems)
                    if ((int)item.Tag == model.Id)
                    {
                        regularItems.Remove(item);
                        DisposeItem(item);
                        break;
                    }

            regularItems.Sort((x, y) => string.Compare(x.Text, y.Text));
            Redraw();
        }

        public void Clear()
        {
            foreach (var item in regularItems)
                DisposeItem(item);

            regularItems.Clear();
            Redraw();
        }

        virtual protected ToolStripMenuItem ModelToItem(ModelType model)
        {
            return new ToolStripMenuItem(model.Name)
            {
                Tag = model.Id,
            };
        }

        /**
         * If a bitmap was created for item icon, dispose it.
         */
        protected virtual void DisposeItem(ToolStripMenuItem item)
        {
            item.Dispose();
        }

        virtual public bool SetSelectedItem(string name)
        {
            bool itemChecked = false;

            foreach (ToolStripMenuItem item in regularItems)
            {
                item.Checked = item.Text == name;

                if (item.Checked)
                {
                    UpdateTitle(item.Text);
                    itemChecked = true;
                }
            }

            if (!itemChecked) UpdateTitle(null);

            return itemChecked;
        }

        virtual public bool SetSelectedItem(int id)
        {
            bool itemChecked = false;

            foreach (ToolStripMenuItem item in regularItems)
            {
                item.Checked = (int)item.Tag == id;

                if (item.Checked)
                {
                    UpdateTitle(item.Text);
                    itemChecked = true;
                }
            }

            if (!itemChecked) UpdateTitle(null);

            return itemChecked;
        }

        private void UpdateTitle(string selectedName)
        {
            if (selectedName == null)
            {
                Text = $"{Title}";
                return;
            }

            int maxLength = TitleMaxLength;

            if (selectedName.Length > maxLength)
                selectedName = selectedName.Substring(0, maxLength) + "...";

            Text = $"{Title}: {selectedName}";
        }

        override protected void Redraw()
        {
            ContextMenuStrip.Items.Clear();
            ContextMenuStrip.Items.AddRange(regularItems.ToArray());
            if (builtInItems.Count > 0 && regularItems.Count > 0)
                ContextMenuStrip.Items.Add(new ToolStripSeparator());
            ContextMenuStrip.Items.AddRange(builtInItems.ToArray());
        }

        protected void EnableItem(ToolStripMenuItem item, bool enabled)
        {
            item.Font = new Font(item.Font, enabled ? FontStyle.Regular : FontStyle.Italic);
            item.Enabled = enabled;
        }

        private void ItemClicked(object sender, EventArgs e)
        {
            DispatchEvent(new MenuButtonEventArgs(EVENT_ITEM_CLICKED, (int)((ToolStripMenuItem)sender).Tag, ((ToolStripMenuItem)sender).Text));
        }
    }

    public class ContextMenuViewItem
    {
        public ContextMenuViewItem(int id, string name)
        {
            Name = name;
            Id = id;
        }

        public ContextMenuViewItem(int id, string name, Image image)
        {
            Name = name;
            Image = image;
            Id = id;
        }

        public string Name { get; }
        public Image Image { get; }
        public int Id { get; }
    }
}
