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

using KinectControll.Manager.Pose;

// Singleton Pattern
using KinectControll.Pattern;

namespace KinectControll.Model.Pose
{
    class PoseModel
    {
        // Stores added poses
        private List<PoseItem> _poseList;
        private String removeType;
                
        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        public PoseModel() 
        {
            _poseList = new List<PoseItem>();
        }
        
        public static PoseModel Instance
        {
            get
            {
                return SingletonProvider<PoseModel>.Instance;
            }
        }
        #endregion

        /**
         * Returns a list of registered poses
         */
        public List<PoseItem> PoseList()
        {
            return _poseList;
        }

        /**
         * Registers a new pose
         * A pose can not be registered twice. The returned pose might have therefore been registered by another class
         */
        public PoseItem RegisterPose(IPose pose)
        {
            PoseItem newItem = null;
            _poseList.ForEach(
                delegate(PoseItem item)
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
                newItem = new PoseItem(pose);
                _poseList.Add(newItem);
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
         * Removes a type of pose from model
         */
        public void removePose(IPose pose)
        {
            removeType = pose.Name();
            _poseList.RemoveAll(HasName);
        }

        public Boolean HasName(PoseItem pose)
        {
            return (pose.Pose.Name() == removeType);
        }
    }
}
