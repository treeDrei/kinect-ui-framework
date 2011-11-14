using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Requzired for Position extension and Vectors
using KinectControll.Model.Position;



namespace KinectControll.Model.Alignment
{
    public class CameraVO : PositionVO
    {
        private int _angle;

        public CameraVO(int angle, PositionVector head, PositionVector leftHand, PositionVector rightHand) : base(head, leftHand, rightHand)
        {
            _angle = angle;
        }

        /**
         * Camera angle
         */
        public int Angle 
        {
            get
            {
                return _angle;
            }
        }
    }
}
