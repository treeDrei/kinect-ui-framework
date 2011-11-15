using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using Microsoft.Research.Kinect.Audio;

using KinectControll.Pattern;

using KinectControll.Manager.Input;
using KinectControll.Manager.Pose;
using KinectControll.Manager.Alignment;
using KinectControll.Manager.Data;

// Required for Alignment manipulation
using KinectControll.Controller.Alignment;

namespace KinectControll.Manager
{
    public class KinectManager
    {
        // Control Manager handles hand position and decides wich one to use
        private HandInputManager UserInputManager;
        // Collision Manager handles control collision with items
        private CollisionManager collisionManager;
        // Pose manager analysisi angles between head and both hands to check for poses
        private PoseManager poseManager;
        // Data manager 
        private SDKDataManager kinectDataManager;

        /**
         * Constructor can only be called by nested class because it's not public
         */
        public KinectManager()
        {

            kinectDataManager = SDKDataManager.Instance;
            // Start data output
            kinectDataManager.Start();
            // Creates input manager
            UserInputManager = HandInputManager.Instance;
            UserInputManager.Start();
            // Creates pose manager
            
            poseManager = PoseManager.Instance;
            //poseManager.CalculateAngles(0, 20, -10, 0, 10, 0);
            /*
            KinectPoseItem idleItem = poseManager.RegisterPose(new Idle());
            // Event handling for idle events
            idleItem.OnPoseBegin += new EventHandler(IdleBeginHandler);
            idleItem.OnPoseComplete += new EventHandler(IdleEndHandler);
            idleItem.OnPoseFailed +=new EventHandler(IdleEndHandler);
            */

            // Creates collision manager
            collisionManager = CollisionManager.Instance;

            //Initialize();

            // Create alignment manager
            /*
            alignmentManager = KinectAlignmentManager.Instance;
            alignmentManager.OnAlignmentRequest += new EventHandler(AlignmentRequestHandler);

            alignmentManager.Arrange();
             */

            // Call of static function to request camera alignment
            AlignmentController.CollectData();
        }

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
    }
}
