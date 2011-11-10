using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Data
{
    public class Position
    {
        public Position(float x = 0, float y = 0, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
