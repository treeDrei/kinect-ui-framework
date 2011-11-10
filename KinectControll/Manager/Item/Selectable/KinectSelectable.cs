using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Item.Selectable
{
    class KinectSelectable : ItemDecorator
    {
        // Event allowing to register on it
        public event EventHandler OnSelection;
        public event EventHandler OnDeselection;

        // Stores count data to match threshold
        private int hitCount = 0;
        // Threshold value 
        private int hitThreshold;
        // Stores selection state
        private Boolean isSelected = false;

        public KinectSelectable(KinectItem kinectItem, int hitThreshold = 20) : base (kinectItem)
        {
            this.hitThreshold = hitThreshold;
        }

        #region Event Trigger
        /**
         * Event dispatch trigger
         * Fill with EventArgs.Empty if nothing is beeing attached
         */
        protected virtual void TriggerSelection(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnSelection;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /**
         * Event dispatch trigger
         * Fill with EventArgs.Empty if nothing is beeing attached
         */
        protected virtual void TriggerDeSelection(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnDeselection;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        /**
         * Extends basic hit setter to allow 
         */
        override public void SetHit(Boolean value)
        {
            // Check hit. Hand over selectable
            if (value)
            {
                // Increase value to build up to threshold
                hitCount++;
            }
            else
            {
                // Reset count to build up again
                hitCount = 0;
                if (isSelected)
                {
                    // Trigger deselection event if item had been selecteds
                    TriggerDeSelection(EventArgs.Empty);
                }
                // item isn't beeing selected
                isSelected = false;
            }

            // Set value via super function
            base.SetHit(value);

            // Show selection if threshold is met
            if (hitCount == hitThreshold)
            {
                // Trigger event. There is no payload to attach here
                TriggerSelection(EventArgs.Empty);
                // This item has just been selected
                isSelected = true;
            }
        }
    }
}
