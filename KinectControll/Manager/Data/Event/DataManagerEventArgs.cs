using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use a vector
using System.Windows;

namespace KinectControll.Manager.Data.Event
{
    public class DataManagerEventArgs : EventArgs
    {
        public DataManagerEventArgs(Position head = null, Position leftHand = null, Position rightHand = null)
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
