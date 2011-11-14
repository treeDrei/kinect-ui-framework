using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Position
{
    public class PositionVector
    {
        public PositionVector(double x = 0, double y = 0, double z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
