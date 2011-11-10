using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using Microsoft.Research.Kinect.Audio;

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
        //
        private KinectAlignmentManager alignmentManager;
        // Control Manager handles hand position and decides wich one to use
        private KinectInputManager inputManager;
        // Collision Manager handles control collision with items
        private CollisionManager collisionManager;
        /*
        // Natual user interface (Kinect)
        private Runtime runtime;
        */
        // Pose manager analysisi angles between head and both hands to check for poses
        private KinectPoseManager poseManager;
        // Data manager 
        private KinectDataManager kinectDataManager;

        //private Boolean isIdle = false;
        
        // How many frame shall be waited to do the next pose check
        //private const int POSE_DELAY = 10;
        //private int poseCount = 0;

        // Current angle
        private int angle;

        /**
         * Constructor can only be called by nested class because it's not public
         */
        KinectManager()
        {
            kinectDataManager = KinectDataManager.Instance;
            // Start data output
            kinectDataManager.Start();
            /*
            // Natural User Interface Runtime            
            runtime = new Runtime();
            // Adds eventListener for skeleton frame ready
            runtime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(skeletonFrameReadyHandler);
            */
            // Creates input manager
            inputManager = KinectInputManager.Instance;
            inputManager.Start();
            // Creates pose manager
            /*
            poseManager = KinectPoseManager.Instance;
            //poseManager.CalculateAngles(0, 20, -10, 0, 10, 0);
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
         * Starts to change camera angle to find perfect view 
         */
        void AlignmentRequestHandler(object sender, EventArgs e)
        {
            angle = -20;
        }

        /**
         * Enables position output
         */
        void IdleEndHandler(object sender, EventArgs e)
        {
            //isIdle = false;
            inputManager.Start();
        }

        /**
         * Disables position output on idle begin
         */
        private void IdleBeginHandler(object sender, EventArgs e)
        {
            //isIdle = true;
            inputManager.Stop();
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
                    return Nested.instance;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Du musst den Strom stecker der Kinect einstöpseln!... Meine Fresse...");
                    Console.WriteLine(e.Message);
                }
                return null;
            }
        }

        /**
         * Nested calss can instantiate singleton
         */
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly KinectManager instance = new KinectManager();
        }

        /**
         * Handles skeleton data
         */
        private void skeletonFrameReadyHandler(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Up to 8 skeletons will be found by kniect. Only two can be tracked
            SkeletonFrame allSkeletons = e.SkeletonFrame;

            // Get first tracked skeleton
            SkeletonData firstSkeleton = (from s in allSkeletons.Skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();

            if (angle < 20)
            {
                alignmentManager.checkAngle(angle, firstSkeleton);
                angle++;
            }
            else if (angle == 20)
            {
                alignmentManager.SetBestAngle();
            }

            if (firstSkeleton != null)
            {
                // Output is only enabled if idle isn't met
                /*
                if (!isIdle)
                {
                    // Set data for input evaluation
                    //inputManager.SetInputJoints(firstSkeleton.Joints[JointID.HandLeft], firstSkeleton.Joints[JointID.HandRight]);
                }
                 * */

                /*
                poseCount++;
                // Check only every X. frame
                if (poseCount >= POSE_DELAY)
                {
                    // Start counting again
                    poseCount = 0;
                    // Pose manager need all information
                    poseManager.SetPoseJoints(firstSkeleton.Joints[JointID.Head], firstSkeleton.Joints[JointID.HandLeft], firstSkeleton.Joints[JointID.HandRight]);
                }
                 * */

                
            }

        }

        /**
         * Returns the global Runtime instance
         */
        /*
        public Runtime Runtime
        {
            get
            {
                return runtime;
            }
        }
        */ 

        /**
         * Initializes reuired listeners
         */
        /*
        public void Initialize()
        {
            // Apply multiple options using the "|" pipe as seperator
            // Will enable color, skeleton an depth data from kinect
            runtime.Initialize(RuntimeOptions.UseColor | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseDepthAndPlayerIndex);
           
            // Number of images required
            int poolSize = 2;
            // Opens video stream from hardware to catch regular images
            runtime.VideoStream.Open(ImageStreamType.Video, poolSize, ImageResolution.Resolution640x480, ImageType.Color);
            // Opens depth video stream from kinect hardware
            runtime.DepthStream.Open(ImageStreamType.Depth, poolSize, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);

            // Enables default smoothing
            SetSmoothing();
        }
         */

        /**
         * Allows smoothing manipulation
         */
        /*
        public void SetSmoothing(float smoothing = 0.75f, float correction = 0.0f, float prediction = 0.0f, float jitterRadius = 0.05f, float maxDeviationRadius = 0.04f)
        {
            //Must set to true and set after call to Initialize
            runtime.SkeletonEngine.TransformSmooth = true;

            //Use to transform and reduce jitter
            var parameters = new TransformSmoothParameters
            {
                Smoothing = smoothing,
                Correction = correction,
                Prediction = prediction,
                JitterRadius = jitterRadius,
                MaxDeviationRadius = maxDeviationRadius
            };

            runtime.SkeletonEngine.SmoothParameters = parameters;
        }
         */


    }
}
