using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Pose
{
    class KinectPoseItem
    {
        private IPose pose;
        private int counter = 0;

        /**
         * Stores a pose and checks wether it hase been activated
         */
        public KinectPoseItem(IPose pose)
        {
            this.pose = pose;
        }

        /**
         * Allows pose selection if parameters are matched
         */
        public Boolean Select(Boolean value)
        {
            //Console.WriteLine("Selected: " + this.pose.Name() + " - " + value);
            // Increase counter on select
            if (value)
            {
                if (counter == 0)
                {
                    // Show that pose has begun
                    TriggerPoseBegin(EventArgs.Empty);
                }
                counter++;
            }
            else
            {
                // Pose must have been begun to allow failing
                if (counter > 0)
                {
                    TriggerPoseFail(EventArgs.Empty);
                }
                counter = 0;
            }

            if (counter == pose.Threshold())
            {
                counter = 0;
                TriggerPoseComplete(EventArgs.Empty);
                return true;
            }

            return false;
        }

        /**
         * returns pose
         */
        public IPose Pose
        {
            get
            {
                return pose;
            }
        }

        #region Event
        // The Event will allow external objects to rigister on it
        public event EventHandler OnPoseComplete;
        public event EventHandler OnPoseFailed;
        public event EventHandler OnPoseBegin;

        /**
         * Event dispatch trigger
         */
        protected virtual void TriggerPoseBegin(EventArgs args)
        {
            Console.WriteLine("pose " + pose.Name() + " begun");
            // This copy will make it more thread-safe
            EventHandler handler = OnPoseBegin;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        /**
         * Event dispatch trigger
         */
        protected virtual void TriggerPoseComplete(EventArgs args)
        {
            Console.WriteLine("pose " + pose.Name() + " has been completed");
            // This copy will make it more thread-safe
            EventHandler handler = OnPoseComplete;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        /**
         * Event dispatch trigger
         */
        protected virtual void TriggerPoseFail(EventArgs args)
        {
            Console.WriteLine("pose " + pose.Name() + " has failed");

            // This copy will make it more thread-safe
            EventHandler handler = OnPoseFailed;
            if (handler != null)
            {
                handler(this, args);
            }
        }
        #endregion
    }
}
