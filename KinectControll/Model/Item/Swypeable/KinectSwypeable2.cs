using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Model.Input;
using KinectControll.Model.Item.Swypeable.Event;

using KinectControll.Check.Distance;

namespace KinectControll.Model.Item.Swypable
{
    /**
     * Dragable Class extends ItemDecorator
     */
    class KinectSwypeable2 : ItemDecorator
    {
        //HandInputManager input;

        // Stores count data to match threshold
        private int hitCount = 0;
        // Threshold value 
        private int hitThreshold;
        // Stores selection state
        private Boolean isSelected = false;

        private DistanceCheck distanceCheck;

        /**
         * Super contructor is called by : base(value)
         */
        public KinectSwypeable2(KinectItem item, int upDist, int sideDist, int threshold = 5)
            : base(item)
        {
            distanceCheck = new DistanceCheck(sideDist, upDist);
            //input = HandInputManager.Instance;

            hitThreshold = threshold;
        }

        #region Event Triggers
        // Event allowing to register on it
        public event EventHandler OnSwypeStart;
        public event EventHandler<KinectSwypeableEventArgs> OnSwypeProgress;
        public event EventHandler OnSwypeComplete;

        /**
        * Event dispatch trigger
        * Fill with EventArgs.Empty if nothing is beeing attached
        */
        protected virtual void TriggerSwypeStart(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnSwypeStart;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /**
         * Event dispatch trigger
         * Fill with EventArgs.Empty if nothing is beeing attached
         */
        protected virtual void TriggerSwypeComplete(EventArgs e)    // the Trigger
        {
            EventHandler handler = OnSwypeComplete;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /**
         * Dispatches progress of swype gesture
         */
        protected virtual void TriggerSwypeProgress()    // the Trigger
        {


            EventHandler<KinectSwypeableEventArgs> handler = OnSwypeProgress;   // make a copy to be more thread-safe
            if (handler != null)
            {
                KinectSwypeableEventArgs args = new KinectSwypeableEventArgs(); 
                //args.horizontalDistance = 
                handler(this, args);
            }
        }

        #endregion

        /**
         * Extends basic hit setter to allow 
         */
        override public void SetHit(Boolean value)
        {
            // Progress input
            if (isSelected && value)
            {
                // Progress input
                ProgressInput();
            }
            // Check selection / deselection
            else
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
                    isSelected = false;
                }

                // Set value via super function
                base.SetHit(value);

                // Show selection if threshold is met
                if (hitCount == hitThreshold)
                {
                    ProgressSelection();
                }
            }
        }

        /**
         * Stores hand start point to check distances
         */
        private void ProgressSelection()
        {
            // Trigger event. There is no payload to attach here
            TriggerSwypeStart(EventArgs.Empty);
            // This item has just been selected
            isSelected = true;
            // Start distance check from current input point
            distanceCheck.Start = InputModel.Instance.Current;
        }

        /**
         * Progresses Input
         */
        private void ProgressInput()
        {
            // If check is complete
            if (distanceCheck.SetCurrent(InputModel.Instance.Current))
            {
                isSelected = false;
                TriggerSwypeComplete(EventArgs.Empty);
            }
            else
            {
                TriggerSwypeProgress();
            }
        }
    }
}
