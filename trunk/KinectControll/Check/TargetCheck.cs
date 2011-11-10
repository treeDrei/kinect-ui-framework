using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace KinectControll.Check
{
    class TargetCheck
    {
        // Target point to be met with current point
        Point target;
        // Radius to allw current point in to be near radius
        int radius;

        /**
         * Target and radius must be set
         */
        public TargetCheck(Point target, int radius)
        {
            this.target = target;
            this.radius = radius;
        }

        /**
         * Returns true if current point is near target
         */
        public Boolean SetCurrent(Point current)
        {
            // Return true if in radius r around P(x,y)
            return ((target.Y > current.Y - radius && target.Y < current.Y + radius) && (target.X > current.X - radius && target.X < current.X + radius));
        }
    }
}