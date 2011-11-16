using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Item
{
    /**
     * Dragable Class extends ItemDecorator
     */
    class Dragable : ItemDecorator
    {
        // Event allowing to register on it
        public event EventHandler OnDragStart;
        public event EventHandler OnDragComplete;

        // Stores count data to match threshold
        private int hitCount = 0;
        // Threshold value 
        private int hitThreshold;
        // Stores selection state
        //private Boolean isSelected;

        /**
         * Super contructor is called by : base(value)
         */
        public Dragable(KinectItem item, int threshold = 5)
            : base(item)
        {
            //isSelected = false;
            hitThreshold = threshold;
        }

        #region Event Triggers

        /**
        * Event dispatch trigger
        * Fill with EventArgs.Empty if nothing is beeing attached
        */
        protected virtual void TriggerDragStart(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnDragStart;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /**
         * Event dispatch trigger
         * Fill with EventArgs.Empty if nothing is beeing attached
         */
        protected virtual void TriggerDragComplete(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnDragComplete;   // make a copy to be more thread-safe
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
                // item isn't beeing selected
                //isSelected = false;
            }

            // Set value via super function
            base.SetHit(value);

            // Show selection if threshold is met
            if (hitCount == hitThreshold)
            {
                // Trigger event. There is no payload to attach here
                TriggerDragStart(EventArgs.Empty);
                // This item has just been selected
                //isSelected = true;
            }
        }
    }
}
