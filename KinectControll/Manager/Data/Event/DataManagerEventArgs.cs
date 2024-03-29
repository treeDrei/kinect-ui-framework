﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use a vector
using System.Windows;

// Required for positions
using KinectControll.Model.Position;

namespace KinectControll.Manager.Data.Event
{
    public class DataManagerEventArgs : EventArgs
    {
        public DataManagerEventArgs(PositionVector head = null, PositionVector leftHand = null, PositionVector rightHand = null)
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
