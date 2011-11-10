using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager.Input.Event
{
    public class KinectInputManagerEventArgs : EventArgs
    {
        public double xPos { get; set; }
        public double yPos { get; set; }
    }
}
