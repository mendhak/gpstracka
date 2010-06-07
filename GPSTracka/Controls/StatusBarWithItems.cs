using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GPSTracka
{
    /// <summary><see cref="StatusBar"/> with intems</summary>
    class ItemsStatusBar : Label
    {
        public ItemsStatusBar()
            : base()
        {
        }
        /// <summary>Contains status bar items</summary>
        private List<string> items = new List<string>();
        /// <summary>Status bar items</summary>
        public string this[int index]
        {
            get { return items[index]; }
            set
            {
                if (index == items.Count)
                    items.Add(value);
                else
                {
                    if (items[index] == value) return;
                    items[index] = value;
                }
                RefreshText();
            }
        }
        /// <summary>Refreshes value of the <see cref="Control.Text"/> property from items</summary>
        private void RefreshText()
        {
            System.Text.StringBuilder text = new StringBuilder();
            foreach (string item in items)
                if (!string.IsNullOrEmpty(item))
                    text.Append((text.Length == 0 ? "" : " | ") + item);
            this.Text = text.ToString();
        }
        /// <summary>Gets or sets all the items of status bar as array of strings</summary>
        public string[] Items
        {
            get { return items.ToArray(); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                items = new List<string>(value);
                RefreshText();
            }
        }
        /// <summary>Gets or sets actual count of status bar items</summary>
        public int ItemsCount
        {
            get { return items.Count; }
            set
            {
                if (value > items.Count || value < 0) throw new ArgumentOutOfRangeException("value", "Items count cannot be negative and it cannot be increased via ItemsCount property.");
                if (value == 0) items.Clear();
                else if (value < items.Count) items.RemoveRange(items.Count - value, value);
            }
        }
    }
}
