using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Check.Speed
{
    class HorizontalSpeedCheck : SpeedCheck
    {
        private String direction;

        public HorizontalSpeedCheck(int distance, int time, String direction)
            : base(distance, time)
        {
            this.direction = direction; 
        }

        /**
         * Checks only horizontal speed
         */
        override protected Boolean checkDistance(int startIndex)
        {
            double distX = 0;
            
            int delay = 0;

            // Run through all checks and add distance and time up
            for (int i = 0; i < compareDataLength; i++)
            {
                // Adds up to current index but will avoid circle errors
                if (i + startIndex == index)
                {
                    return false;
                }

                // Either x or y could meet distance requirement
                distX += positionList[(i + startIndex) % compareDataLength].X;

                // Delay will be added up
                delay += delayList[i];

                // Check wether distance has been met in time
                if (delay <= time && (distX >= distance))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
