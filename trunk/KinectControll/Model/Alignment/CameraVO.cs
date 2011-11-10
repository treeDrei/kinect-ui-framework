using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Manager.Data;

namespace KinectControll.Model.Alignment
{
    public class CameraVO
    {
        private int _angle;

        public CameraVO(int angle, Position head, Position leftHand, Position rightHand)
        {
            _angle = angle;
            this.Head = head;
            this.LeftHand = leftHand;
            this.RightHand = rightHand;
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

        /**
         * Head position
         * Get/set
         */
        public Position Head { get; set; }

        /**
         * Left hand position
         * Get/set
         */
        public Position LeftHand { get; set; }

        /**
         * Right hand position
         * Get/set
         */
        public Position RightHand { get; set; }
    }
}
