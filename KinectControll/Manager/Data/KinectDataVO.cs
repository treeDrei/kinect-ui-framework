using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Data
{
    public class KinectDataVO
    {
        public KinectDataVO(Position head = null, Position leftHand = null, Position rightHand = null)
        {
            this.Head = head;
            this.LeftHand = leftHand;
            this.RightHand = rightHand;
        }

        public Position Head { get; set; }
        public Position LeftHand { get; set; }
        public Position RightHand { get; set; }
    }
}
