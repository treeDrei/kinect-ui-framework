using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

// Required for Timer
using System.Timers;
using KinectControll.Check;

namespace KinectControll.Check.Speed
{
    class SpeedCheck : ICheck
    {
        // List of points in 2 dimensional space
        protected Point[] positionList;
        // Delay between point update
        protected int[] delayList;
        // 
        protected int compareDataLength;

        protected int index;

        protected int time;
        protected int distance;

        // Timer for delay storage
        private Timer delayTimer;
        // Stores timer delay
        private int timerDelay;

        /**
         * Distance and the time in wich it will be taken must be set
         */
        public SpeedCheck(int distance, int time)
        {
            CreateTiemr();

            compareDataLength = 10;
            index = 0;
            this.distance = distance;
            this.time = time;

            positionList = new Point[compareDataLength];
            delayList = new int[compareDataLength];
            // start point at 0,0
            positionList[compareDataLength-1] = new Point(0, 0);
            // Fill lists with default data
            Clear();

            
        }

        /**
         * Initializes timer object
         */
        private void CreateTiemr()
        {
            timerDelay = 0;

            delayTimer = new Timer(100);
            delayTimer.Elapsed += new ElapsedEventHandler(TimerTickHandler);
            delayTimer.Enabled = true;
        }

        /**
         * Timer tick handler
         */
        void TimerTickHandler(object sender, ElapsedEventArgs e)
        {
            timerDelay += 100;
        }


        /**
         * Returns true if current point is near target
         * delay between two point sets
         * current is the current hand location
         */
        public Boolean SetCurrent(Point current)
        {
            // Update list
            positionList[(index) % compareDataLength] = current;
            delayList[(index++) % compareDataLength] = timerDelay;

            // Reset timer delay for next setting
            timerDelay = 0;

            for (int i = 0; i < compareDataLength-1; i++)
            {
                // Check each distance and return true if speed is met
                if (checkDistance(i))
                {
                    return true;
                }
            }

            // Return false if no speed check had been true
            return false;
        }

        /**
         * Summs up distances and time from start index to see wether speed has been checked 
         */
        virtual protected Boolean checkDistance(int startIndex)
        {
            double distX = 0;
            double distY = 0;

            int delay = 0;

            // Run through all checks and add distance and time up
            for (int i = 1; i < compareDataLength; i++)
            {
                // Adds up to current index but will avoid circle errors
                if (i + startIndex == index)
                {
                    return false;
                }

                // Either x or y could meet distance requirement

                distX += positionList[(i + startIndex) % compareDataLength].X - positionList[(i-1 + startIndex) % compareDataLength].X;
                distY += positionList[(i + startIndex) % compareDataLength].Y - positionList[(i-1 + startIndex) % compareDataLength].Y;

                // Delay will be added up
                delay += delayList[i];

                // Check wether distance has been met in time
                if (delay <= time && (distX >= distance || distY >= distance))
                {
                    return true;
                }
            }

            return false;

        }

        /**
         * Clears stored data
         */
        public void Clear()
        {
            timerDelay = 0;

            // Stupid modulo will not work
            int lastPointIndex = (index - 1) % compareDataLength;
            if (lastPointIndex < 0)
            {
                lastPointIndex += compareDataLength;
            }
            Point lastPoint = positionList[lastPointIndex];
            for (var i = 0; i < compareDataLength; i++)
            {
                positionList[i] = new Point(lastPoint.X, lastPoint.Y);
                delayList[i] = time*10;
            }
        }

        /**
         * Stops
         */
        public void Stop()
        {
            timerDelay = 0;
            delayTimer.Stop();
        }

        /**
         * Starts
         */
        public void Start()
        {
            timerDelay = 0;
            delayTimer.Stop();
        }
    }
}
