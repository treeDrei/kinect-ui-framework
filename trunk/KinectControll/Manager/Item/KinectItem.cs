using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using KinectControll.Manager.Item;

namespace KinectControll.Manager.Item
{
    public class KinectItem
    {
        private double width;
        private double height;
        private double x;
        private double y;
        private Boolean enabled = true;
        private Boolean hit = false;

        private String id;

        // Debug info will be dispatched if debug is enabled
        public event EventHandler<EventArgs> OnEnabledChange;

        //KinectItemManager itemManager;

        public KinectItem(String id, double x, double y, double width, double height)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        #region Events
        /**
         * Triggers Enabled change event
         */
        protected void TriggerEnabledChange(EventArgs args)
        {
            EventHandler<EventArgs> handler = OnEnabledChange;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        /**
         * Returns id, wich allows assignment to ui elements
         */
        public virtual String GetID()
        {
            return id;
        }

        /**
         * Beeing set if hand hits item
         */
        public virtual void SetHit(Boolean value)
        {
            hit = value;
        }

        /**
         * Height getter
         */
        public virtual double GetWidth()
        {
            return width;
        }

        public virtual void SetWidth(double value)
        {
            width = value;
        }

        /**
         * Height set & get
         */
        public virtual double GetHeight()
        {
            return height;
        }

        public virtual void SetHeight(double value)
        {
            height = value;
        }

        /**
         * X set & get
         */
        public virtual double GetX()
        {
            return x;
        }

        public virtual void SetX(double value)
        {
            x = value;
        }

        /**
         * Y set & get
         */
        public virtual double GetY()
        {
            return y;
        }

        public virtual void SetY(double value)
        {
            y = value;
        }

        /**
         * Enabled set & get
         */
        public virtual Boolean GetEnabled()
        {
            return enabled;
        }

        /**
         * Sets wether this item should be interactable
         */
        public virtual void SetEnabled(Boolean value)
        {
            enabled = value;
            TriggerEnabledChange(EventArgs.Empty);
        }
    }
}
