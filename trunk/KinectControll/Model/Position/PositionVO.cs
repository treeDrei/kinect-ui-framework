using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Position
{
    public class PositionVO
    {
        public PositionVO(PositionVector head = null, PositionVector leftHand = null, PositionVector rightHand = null)
        {
            this.Head = head;
            this.LeftHand = leftHand;
            this.RightHand = rightHand;
        }

        public PositionVector Head { get; set; }
        public PositionVector LeftHand { get; set; }
        public PositionVector RightHand { get; set; }
    }
}
