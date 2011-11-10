using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace KinectControll.Check.Distance
{
    class DistanceCheck : ICheck
    {
        private Point start;
        private double horizontalDistance;
        private double verticalDistance;

        public DistanceCheck(double verticalDistance, double horizontalDistance, Point start = new Point())
        {
            this.start = start;
            this.verticalDistance = verticalDistance;
            this.horizontalDistance = horizontalDistance;
        }

        /**
         * Returns true if current point is near target
         */
        public Boolean SetCurrent(Point current)
        {
            double horDist = start.X - current.X;
            double vertDist = start.Y - current.Y;
            Boolean horizontal = ((horDist > horizontalDistance) || horizontalDistance == 0);
            Boolean vertical = ((vertDist > verticalDistance) || verticalDistance == 0);
            if (verticalDistance < 0)
            {
                vertical = (vertDist < verticalDistance);
            }
            if(horizontalDistance < 0 )
            {
                horizontal = (horDist < horizontalDistance);
            }

            Console.WriteLine("vert = " + vertDist + ", " + verticalDistance + ", " + vertical + " /  hor = " + horDist + ", " + horizontalDistance + ", " + horizontal);
            // Return true if in radius r around P(x,y)
            // Dont check up/down if the distance is 0
            return (vertical && horizontal);
        }

        /**
         * Start point from where to check distance can be set and got at any given time 
         */
        public Point Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }
    }
}
