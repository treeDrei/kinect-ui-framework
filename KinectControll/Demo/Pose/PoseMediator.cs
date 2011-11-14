using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Manager.Pose;
using KinectControll.Model.Pose;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace KinectControll.Demo.Pose
{
    class PoseMediator: Notifier, INotifier
    {
        private PoseItem _poseItem;

        public PoseMediator(IPose pose)
        {
            _poseItem = PoseModel.Instance.RegisterPose(pose);
            OnRegister();
        }

        /**
         * This pose is beeing mediated by the pose Mediator
         */
        public PoseItem PoseItem
        {
            get
            {
                return _poseItem;
            }
        }

        /**
         * Registers 
         */
        public virtual void OnRegister()
        {

        }
    }
}
