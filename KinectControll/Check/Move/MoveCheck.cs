using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for Point 
using System.Windows;

using KinectControll.Check.Distance;
using KinectControll.Check.Speed;

namespace KinectControll.Check.Move
{
    class MoveCheck : ICheck
    {
        private SpeedCheck speedCheck;
        private DistanceCheck distanceCheck;

        public MoveCheck(SpeedCheck speedCheck, DistanceCheck distanceCheck)
        {
            this.speedCheck = speedCheck;
            this.distanceCheck = distanceCheck;
        }

        /**
         * Returns true if current point is near target
         */
        public Boolean SetCurrent(Point current)
        {
            Boolean speed = speedCheck.SetCurrent(current);
            if (!speed)
            {
                // Update distance check start point as long as speed check isn't meet
                distanceCheck.Start = current;
            }
            // If speed is fast enough 
            else
            {
                // Check wether distance is long enough, too
                if (distanceCheck.SetCurrent(current))
                {
                    speedCheck.Clear();
                    return true;
                }
            }

            // Move criteria hasn't been met
            return false;
        }

        /**
         * Allows speed check parameter manipulation
         */
        public SpeedCheck SpeedCheck
        {
            get
            {
                return speedCheck;
            }
            set
            {
                speedCheck = value;
            }
        }

        /**
         * Allows distance check parameter manipulation
         */
        public DistanceCheck DistanceCheck
        {
            get
            {
                return distanceCheck;
            }
            set
            {
                distanceCheck = value;
            }
        }
    }
}
