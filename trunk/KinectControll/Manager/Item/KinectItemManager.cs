using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Item
{
    class KinectItemManager
    {

        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectItemManager() { }

        public static KinectItemManager Instance
        {
            get
            {
                return SingletonCreator.instance;
            }
        }

        /**
         * Nested calss can only be called by ItemManager
         */
        private class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly KinectItemManager instance = new KinectItemManager();
        }
        #endregion

        #region Private and public variables
        // List of handled objects
        private List<KinectItem> items = new List<KinectItem>();
        private List<KinectItem> enabled = new List<KinectItem>();
        #endregion
        
        /**
         * Adds an item to allow interaction with
         */
        public Boolean RegisterItem(KinectItem item)
        {
            // Existing items won't be added twice
            if (!items.Contains(item))
            {
                item.OnEnabledChange += new EventHandler<EventArgs>(EnabledChangeHandler);
                items.Add(item);
                if (item.GetEnabled())
                {
                    enabled.Add(item);
                }
            }

            // Shows wether a new item has been added
            return !items.Contains(item);
        }

        /**
         * Returns a list of enabled items
         */
        public List<KinectItem> Enabled
        {
            get
            {
                return enabled;
            }
        }

        #region Event Handling
        /**
         * Adds or removed item on enabled change from enabled list
         * This is beeing done to prevent a list conpareison o fevery item on each Enabled request
         */
        void EnabledChangeHandler(object sender, EventArgs e)
        {
            // Type cast to Kinect item
            KinectItem item = (KinectItem)sender;
            // Chek if item is enabled or already in list
            if (item.GetEnabled() && !enabled.Contains(item))
            {
                // Add item to list
                enabled.Add(item);
            }
            // Double check wether item is in list
            else if(enabled.Contains(item))
            {
                // Remove item from list
                enabled.Remove(item);
            }
        }
        #endregion

    }
}
