using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Item
{
    abstract class ItemDecorator : KinectItem
    {
        protected KinectItem kinectItem;

        public ItemDecorator(KinectItem kinectItem) : base(kinectItem.GetID(), kinectItem.GetX(), kinectItem.GetY(), kinectItem.GetWidth(), kinectItem.GetHeight())
        {
            this.kinectItem = kinectItem;
            kinectItem.OnEnabledChange += new EventHandler<EventArgs>(BubbleEnabledChange);
        }

        /**
         * Bubbles kinect item event to parent object
         */
        void BubbleEnabledChange(object sender, EventArgs e)
        {
            // Trigger base function
            TriggerEnabledChange(e);
        }

        /**
         * Allows ui to item assignment
         */
        public override string GetID()
        {
            return kinectItem.GetID();
        }

        /**
         * Beeing set if hand hits item
         */
        override public void SetHit(Boolean value)
        {
            kinectItem.SetHit(value);
        }

        /**
        * Height getter
        */
        override public double GetWidth()
        {
            return kinectItem.GetWidth();
        }

        override public void SetWidth(double value)
        {
            kinectItem.SetWidth(value);
        }

        /**
         * Height set & get
         */
        override public double GetHeight()
        {
            return kinectItem.GetHeight();
        }

        override public void SetHeight(double value)
        {
                kinectItem.SetHeight(value);
        }

        /**
         * X set & get
         */
        override public double GetX()
        {
            return kinectItem.GetX();
        }

        override public void SetX(double value)
        {
            kinectItem.SetX(value);
        }

        /**
         * Y set & get
         */
        override public double GetY()
        {
            return kinectItem.GetY();
        }

        override public void SetY(double value)
        {
            kinectItem.SetY(value);
        }

        /**
         * Enabled set & get
         */
        override public Boolean GetEnabled()
        {
            return kinectItem.GetEnabled();
        }

        override public void SetEnabled(Boolean value)
        {
            kinectItem.SetEnabled(value);
        }
    }
}
