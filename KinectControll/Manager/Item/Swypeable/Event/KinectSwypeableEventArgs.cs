using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Item.Swypeable.Event
{
    class KinectSwypeableEventArgs : EventArgs
    {
        public double verticalDistance { get; set; }
        public double horizontalDistance { get; set; }
    }
}
