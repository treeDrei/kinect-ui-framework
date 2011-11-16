using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using Microsoft.Research.Kinect.Audio;

using KinectControll.Pattern;

using KinectControll.Manager.Input;
using KinectControll.Manager.Pose;
using KinectControll.Manager.Data;

using KinectControll.Model.Pose;

// Required for Alignment manipulation
using KinectControll.Controller.Alignment;

namespace KinectControll.Manager
{
    public class KinectManager : Manager, IManager
    {
        // Control Manager handles hand position and decides wich one to use
        private HandInputManager UserInputManager;
        // Collision Manager handles control collision with items
        private KinectCollisionManager collisionManager;
        // Pose manager analysisi angles between head and both hands to check for poses
        private PoseManager poseManager;
        // Data manager 
        private SDKDataManager kinectDataManager;

        /**
         * Constructor can only be called by nested class because it's not public
         */
        public KinectManager()
        {
            // Create required managers
            kinectDataManager = SDKDataManager.Instance;
            UserInputManager = HandInputManager.Instance;
            
            poseManager = PoseManager.Instance;
            PoseItem idleItem = PoseModel.Instance.RegisterPose(new IdlePose());

            // Event handling for idle events
            idleItem.OnPoseBegin += new EventHandler(IdleBeginHandler);
            idleItem.OnPoseComplete += new EventHandler(IdleEndHandler);
            idleItem.OnPoseFailed += new EventHandler(IdleEndHandler);
            
            collisionManager = KinectCollisionManager.Instance;
        }

        /**
        * Thread safe singleton intsantiation
        */
        public static KinectManager Instance
        {
            get
            {
                try
                {
                    return SingletonProvider<KinectManager>.Instance;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Du musst den Strom stecker der Kinect einstöpseln!... Meine Fresse...");
                    Console.WriteLine(e.Message);
                }
                return null;
            }
        }

        #region Event handling
        /**
         * Enables position output
         */
        void IdleEndHandler(object sender, EventArgs e)
        {
            UserInputManager.Start();
        }

        /**
         * Disables position output on idle begin
         */
        private void IdleBeginHandler(object sender, EventArgs e)
        {
            UserInputManager.Stop();
        }
        #endregion
        
        #region IManager implementation
        /**
         * Starts all managers
         */
        public void Start()
        {
            if (!isStarted)
            {
                isStarted = true;

                // Start all managers
                kinectDataManager.Start();
                UserInputManager.Start();
                poseManager.Start();
                collisionManager.Start();

            }
        }

        /**
         * Stops all managers
         */
        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;

                // Stop all managers
                kinectDataManager.Stop();
                UserInputManager.Stop();
                poseManager.Stop();
                collisionManager.Stop();
            }
        }
        #endregion
    }
}
