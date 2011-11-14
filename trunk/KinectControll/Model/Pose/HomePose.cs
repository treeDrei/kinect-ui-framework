﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Model.Pose
{
    class HomePose : IPose
    {

        /**
         * This highh value is taken because the pose is not beeing used to trigger a view change
         */
        public int Threshold()
        {
            return 10;
        }

        /**
         * Will be met if left hand is just hanging down
         */
        public double LeftHandAngle()
        {
            return 50;
        }

        /**
         * Will be met if right hand is hanging down
         */
        public double RightHandAngle()
        {
            return 42;
        }

        /**
         * Will work for people of differnt size/weight
         */
        public double AngleOffset()
        {
            return 6;
        }

        /**
         * Hand dist isn't beeing used right now
         */
        public double LeftHandDist()
        {
            return 0;
        }

        /**
         * Hand dist isn't beeing used right now
         */
        public double RightHandDist()
        {
            return 0;
        }


        public double DistOffset()
        {
            return 1;
        }

        /**
         * Allows pose distinguishing
         */
        public String Name()
        {
            return "Home";
        }
    }
}
