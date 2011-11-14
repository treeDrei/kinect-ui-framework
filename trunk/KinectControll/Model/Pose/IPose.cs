using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Pose
{
    interface IPose
    {
        int Threshold();
        
        double LeftHandAngle();
        double RightHandAngle();
        double AngleOffset();

        double LeftHandDist();
        double RightHandDist();
        double DistOffset();

        String Name();
    }
}
