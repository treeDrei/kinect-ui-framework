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

namespace KinectControll.Manager.Pose
{
    class KinectPoseManager
    {
        // Stores added poses
        private List<KinectPoseItem> poseList;
                
        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectPoseManager() 
        {
            poseList = new List<KinectPoseItem>();
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
         * Registers a new pose
         */
        public KinectPoseItem RegisterPose(IPose pose)
        {
            KinectPoseItem newItem = null;
            poseList.ForEach(
                delegate(KinectPoseItem item)
                {
                    // Check wether pose type is matching existing pose item
                    if (item.Pose.GetType() == pose.GetType())
                    {
                        // Assign to return value
                        newItem = item;
                    }
                }
            );

            // This pose hasn't been used before
            if (newItem == null)
            {
                // Create new
                newItem = new KinectPoseItem(pose);
                poseList.Add(newItem);
            }
            
            //Return a new item if this pose hasn't been registered before
            return newItem;

            /*
            // Existing poses won't be added twice
            if (!poseList.Contains(pose))
            {
                poseList.Add(pose);
            }

            // Shows wether a new item has been added
            return !poseList.Contains(pose);
            */
        }
               
        /**
         * 
         */
        public void CalculateAngles(double headX, double headY, double leftX, double leftY, double rightX, double rightY)
        {
            double mHeadLeft = MathUtil.CalculateGradient(headX, headY, leftX, leftY);
            double mRightLeft = MathUtil.CalculateGradient(rightX, rightY, leftX, leftY);
            double mHeadRight = MathUtil.CalculateGradient(headX, headY, rightX, rightY);

            double angleLeft = MathUtil.CalculateAngle(mHeadLeft, mRightLeft);
            double angleRight = MathUtil.CalculateAngle(mHeadRight, mRightLeft);
            double angleHead = 180 - (angleLeft + angleRight);

            // Check all poses 
            poseList.ForEach
            (
                delegate(KinectPoseItem item)
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
