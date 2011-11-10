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
        public static float CalculateGradient(float x1, float y1, float x2, float y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        /**
         * Calculates straight between two 
         */
        public static double CalculateAngle(float m1, float m2)
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
