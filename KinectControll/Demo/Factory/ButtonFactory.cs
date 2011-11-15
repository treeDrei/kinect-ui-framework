using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for Image
using System.Windows.Controls;

// Item Manager
using KinectControll.Manager.Item;
using KinectControll.Manager.Item.Selectable;

namespace KinectControll.Demo.Factory
{
    static class ButtonFactory
    {
        /**
         * Creates a button
         */
        public static KinectSelectable CreateSelectionButton(String name, Image button, int threshold = 10, KinectItem item = null)
        {
            if (item == null)
            {
                item = new KinectItem(name, button.Margin.Left, button.Margin.Top, button.Width, button.Height);
            }

            return new KinectSelectable(item, threshold);
        }

        /**
         * Creates a button
         */
        public static KinectHoldable CreateHoldButton(String name, Image button, int threshold = 10, KinectItem item = null)
        {
            if (item == null)
            {
                item = new KinectItem(name, button.Margin.Left, button.Margin.Top, button.Width, button.Height);
            }

            return new KinectHoldable(item, threshold);
        }
    }
}
