﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Model.Input;
using KinectControll.Model.Item.Swypeable.Event;

// Required for distance / speed check
using KinectControll.Check.Move;

namespace KinectControll.Model.Item.Swypable
{
    /**
     * Dragable Class extends ItemDecorator
     */
    class KinectSwypeable : ItemDecorator
    {
        #region Private variables
        private MoveCheck _moveCheck;
        #endregion

        /**
         * Super contructor is called by : base(value)
         */
        public KinectSwypeable(KinectItem item, MoveCheck moveCheck)
            : base(item)
        {
            // Speed and distance will make up a gesture
            this._moveCheck = moveCheck;          
        }

        
        #region Event Triggers
        // Event allowing to register on it
        public event EventHandler OnSwypeComplete;

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
        #endregion

        /**
         * Extends basic hit setter to allow 
         */
        override public void SetHit(Boolean value)
        {
            //Check wether speed paraneters have been met
            if (_moveCheck.SetCurrent(InputModel.Instance.Current))
            {
                // If parameters have been met
                TriggerSwypeComplete(EventArgs.Empty);
            }

        }

        /**
         * Sets wether this component should be executed
         */
        public override void SetEnabled(bool value)
        {
            // Start/stop timer if enabled/disabled
            if (value)
            {
                //delayTimer.Start();
            }
            else
            {
                //delayTimer.Stop();
            }

            base.SetEnabled(value);
        }
    }
}
