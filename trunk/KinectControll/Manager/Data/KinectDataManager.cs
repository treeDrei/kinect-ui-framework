using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use data manager event araguments
using KinectControll.Manager.Data.Event;
// Allows Singleton instantiation via SingletonProvider
using KinectControll.Pattern;
// Microsoft Kinect API
using Microsoft.Research.Kinect.Nui;
// Stores normed psoition
using KinectControll.Model.Position;
// Required to apply offset
using KinectControll.Model.Alignment;

namespace KinectControll.Manager.Data
{
    public class KinectDataManager : ADataManager, IDataManager
    {

        #region Private variables
        // Natual user interface (Kinect)
        private Runtime _runtime;

        // Update event count
        private int _fixedUpdateDelay = 10;
        private int _fixedUpdateIndex = 0;

        // Stores wether data stream has been opened
        private Boolean _isStarted = false;

        private PositionModel _positionModel;
        #endregion

        /**
         * Initializes Data manager in constructor
         * This is called only once since it should be used as singleton
         */
        public KinectDataManager()
        {
            _runtime = Runtime.Kinects[0];
            _runtime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(SkeletonFrameReadyHandler);

            _positionModel = PositionModel.Instance;
        }

        /**
         * Handles skeleton data
         */
        private void SkeletonFrameReadyHandler(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Up to 8 skeletons will be found by kniect. Only two can be tracked
            SkeletonFrame allSkeletons = e.SkeletonFrame;

            // Get first tracked skeleton
            SkeletonData firstSkeleton = (from s in allSkeletons.Skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();

            if (firstSkeleton != null)
            {
                // Create object with null reference
                PositionVector head = null;
                PositionVector leftHand = null;
                PositionVector rightHand = null;

                // Check wether user is beeing tracked
                if (firstSkeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    // Retreive data from skeleton joint vector
                    head = RetreiveData(firstSkeleton, JointID.Head);           // = new Position(firstSkeleton.Joints[JointID.Head].Position.X, firstSkeleton.Joints[JointID.Head].Position.Y, firstSkeleton.Joints[JointID.Head].Position.Z);
                    leftHand = RetreiveData(firstSkeleton, JointID.HandLeft);   //new Position(firstSkeleton.Joints[JointID.HandLeft].Position.X, firstSkeleton.Joints[JointID.HandLeft].Position.Y, firstSkeleton.Joints[JointID.HandLeft].Position.Z);
                    rightHand = RetreiveData(firstSkeleton, JointID.HandRight); //new Position(firstSkeleton.Joints[JointID.HandRight].Position.X, firstSkeleton.Joints[JointID.HandRight].Position.Y, firstSkeleton.Joints[JointID.HandRight].Position.Z);

                    // Apply offset to data to transform user input into the midle
                    double offsetX = AlignmentModel.Instance.Offset.X;
                    head.X += offsetX;
                    leftHand.X += offsetX;
                    rightHand.X += offsetX;

                    // Positions don't need to be updated if nobody is beeing tracked
                    TriggerUpdate(new DataManagerEventArgs(head, leftHand, rightHand));
                }

                _positionModel.Normed = new PositionVO(head, leftHand, rightHand);
             }
        }

        /**
         * Will factor joint data to internal position data
         */
        private PositionVector RetreiveData(SkeletonData skeletonData, JointID jointID)
        {
            return new PositionVector(skeletonData.Joints[jointID].Position.X, skeletonData.Joints[jointID].Position.Y, skeletonData.Joints[jointID].Position.Z);
        }

        #region Private methods
        /**
         * Allows smoothing manipulation
         */
        public void SetSmoothing(float smoothing = 0.75f, float correction = 0.0f, float prediction = 0.0f, float jitterRadius = 0.05f, float maxDeviationRadius = 0.04f)
        {
            //Must set to true and set after call to Initialize
            _runtime.SkeletonEngine.TransformSmooth = true;

            //Use to transform and reduce jitter
            var parameters = new TransformSmoothParameters
            {
                Smoothing = smoothing,
                Correction = correction,
                Prediction = prediction,
                JitterRadius = jitterRadius,
                MaxDeviationRadius = maxDeviationRadius
            };

            _runtime.SkeletonEngine.SmoothParameters = parameters;
        }
        #endregion

        /**
         * Generates instance 
         */
        public static KinectDataManager Instance
        {
            get
            {
                // Use the Singleton Provider to create an instance of this class or reference created class if already instatiated
                return SingletonProvider<KinectDataManager>.Instance;
            }
        }

        #region Event
        // The Event will allow external objects to rigister on it
        public event EventHandler<DataManagerEventArgs> OnUpdate;
        public event EventHandler<DataManagerEventArgs> OnFixedUpdate;

        /**
         * Trigger an update on every single frame
         * This update is beeing used for smooth animations
         */
        override protected void TriggerUpdate(DataManagerEventArgs args)
        {
            // This copy will make it more thread-safe
            EventHandler<DataManagerEventArgs> handler = OnUpdate;
            if (handler != null)
            {
                handler(this, args);
            }

            _fixedUpdateIndex++;
            // Check wether index is higher than count threshold
            if (_fixedUpdateIndex >= _fixedUpdateDelay)
            {
                // Start counting again
                _fixedUpdateIndex = 0;
                // Trigger fixed update
                TriggerFixedUpdate(args);
            }
        }

        /**
         * Triggers update with certain delay
         * This update is beeing used for calculations, etc.
         */
        override protected void TriggerFixedUpdate(DataManagerEventArgs args)
        {
            // This copy will make it more thread-safe
            EventHandler<DataManagerEventArgs> handler = OnFixedUpdate;
            if (handler != null)
            {
                handler(this, args);
            }
        }
        #endregion

        #region Public methods
        /**
         * Starts data output
         */
        public void Start()
        {
            // Avoids double start
            if (!_isStarted)
            {
                // Store data stream state
                _isStarted = true;

                // Apply multiple options using the "|" pipe as seperator
                // Will enable color, skeleton an depth data from kinect
                _runtime.Initialize(RuntimeOptions.UseColor | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseDepthAndPlayerIndex);

                // Number of images required
                int poolSize = 2;
                // Opens video stream from hardware to catch regular images
                _runtime.VideoStream.Open(ImageStreamType.Video, poolSize, ImageResolution.Resolution640x480, ImageType.Color);
                // Opens depth video stream from kinect hardware
                _runtime.DepthStream.Open(ImageStreamType.Depth, poolSize, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);

                // Enables default smoothing
                SetSmoothing();
            }
        }

        /**
         * Stops data output
         */
        public void Stop()
        {
            // Avoids double stop
            if (_isStarted)
            {
                // Store data stream state
                _isStarted = false;

                // Stop manager
                _runtime.Uninitialize();
            }
        }

        /**
         * Sets delay between fixed updates in frames
         */
        public void SetFixedUpdateDelay(int delay)
        {
            _fixedUpdateDelay = delay;
        }

        /**
         * Getter method for kinect runtime
         */
        public Runtime Runtime
        {
            get
            {
                return _runtime;
            }
        }
        #endregion

       
    }
}
