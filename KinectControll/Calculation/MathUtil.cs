using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Calculation
{
    class MathUtil
    {
        /**
         * Calculates gradient of line between two points
         */
        public static double CalculateGradient(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        /**
         * Calculates straight between two 
         */
        public static double CalculateAngle(double m1, double m2)
        {
            return RadianToDegree(Math.Atan(Math.Abs((m1 - m2) / (1 + (m1 * m2)))));
        }

        /**
         * Turns Radian value to degree
         */
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}
