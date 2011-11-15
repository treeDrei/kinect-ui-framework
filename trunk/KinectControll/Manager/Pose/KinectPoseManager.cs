using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for pase manager event args
using KinectControll.Manager.Pose.Event;
// Required for angle calculation
using KinectControll.Calculation;
// Required 
using Microsoft.Research.Kinect.Nui;
// 
using KinectControll.Manager.Data.Event;
using KinectControll.Manager.Data;
// Required to run through poses
using KinectControll.Model.Pose;

namespace KinectControll.Manager.Pose
{
    class KinectPoseManager
    {
        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectPoseManager() 
        {
            KinectDataManager.Instance.OnFixedUpdate += new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
        }

        /**
         * Handles delayed fixed update
         */
        void FixedUpdateHandler(object sender, DataManagerEventArgs e)
        {
            CalculateAngles(e.Head.X, e.Head.Y, e.LeftHand.X, e.LeftHand.Y, e.RightHand.X, e.RightHand.Y);
        }

        public static KinectPoseManager Instance
        {
            get
            {
                return SingletonCreator.instance;
            }
        }

        /**
         * Nested calss can only be called by ItemManager
         */
        private class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly KinectPoseManager instance = new KinectPoseManager();
        }
        #endregion
               
        /**
         * This amazing mathematic function will take three twodimensional points on an X/Y diagramm and will use them to 
         * calculate the angles between them. These angles make up an triangle wich applied to the human physology can be matched
         * to poses
         */
        public void CalculateAngles(double headX, double headY, double leftX, double leftY, double rightX, double rightY)
        {
            double mHeadLeft = MathUtil.CalculateGradient(headX, headY, leftX, leftY);
            double mRightLeft = MathUtil.CalculateGradient(rightX, rightY, leftX, leftY);
            double mHeadRight = MathUtil.CalculateGradient(headX, headY, rightX, rightY);

            double angleLeft = MathUtil.CalculateAngle(mHeadLeft, mRightLeft);
            double angleRight = MathUtil.CalculateAngle(mHeadRight, mRightLeft);
            // head angle can be calculated with left and right angle. If left and right angles are correct, headn angle has to be also correct.
            //double angleHead = 180 - (angleLeft + angleRight);

            //Console.WriteLine("aL: " + angleLeft + ", aR: " + angleRight);

            // Check all poses 
            PoseModel.Instance.PoseList().ForEach
            (
                delegate(PoseItem item)
                {
                    // Pose retreived for easy angle checking
                    IPose pose = item.Pose;
                    // Enables all Kinect items on this view
                    item.Select(((angleRight > pose.RightHandAngle() - pose.AngleOffset()) && (angleRight < pose.RightHandAngle() + pose.AngleOffset()) && (angleLeft < pose.LeftHandAngle() + pose.AngleOffset()) && (angleLeft > pose.LeftHandAngle() - pose.AngleOffset())));
                }
            );
        }

        #region Event
        // The Event will allow external objects to rigister on it
        public event EventHandler<KinectPoseManagerEventArgs> OnPose;

        /**
         * Event dispatch trigger
         */
        private void TriggerPose( String type )
        {
            // This copy will make it more thread-safe
            EventHandler<KinectPoseManagerEventArgs> handler = OnPose;
            if (handler != null)
            {
                var args = new KinectPoseManagerEventArgs() { type = type };  // vary
                handler(this, args);
            }
        }
        #endregion
    }
}
