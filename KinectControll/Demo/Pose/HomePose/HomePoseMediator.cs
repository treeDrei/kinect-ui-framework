using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use mediator
using KinectControll.Demo.Pose;
// Required for pose
using KinectControll.Model.Pose;

using KinectControll.Demo;

namespace KinectControll.Demo.Pose.HomePose
{
    class HomePoseMediator: PoseMediator
    {
        public HomePoseMediator()
            : base(new KinectControll.Model.Pose.HomePose())
        {
        }

        /**
         * Registers eventhandling on Pose
         */
        public override void OnRegister()
        {
            PoseItem.OnPoseComplete += new EventHandler(HomeCompleteHandler);
        }

        /**
         * Navigate home
         */
        void HomeCompleteHandler(object sender, EventArgs e)
        {
            // Navigate home on pose complete
            SendNotification(DemoApplicationFacade.NAVIGATE_HOME);
        }
    }
}
