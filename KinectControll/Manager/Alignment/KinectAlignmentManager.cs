using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Research.Kinect.Nui;

using KinectControll.Manager.Data;

using KinectControll.Manager.Data.Event;
// Required to start a new thread
using System.Threading;

namespace KinectControll.Manager.Alignment
{
    class KinectAlignmentManager
    {
        private Boolean isChecking = false;

        //private int bestAngle;
        /*
        private int jointAmount;
        private Boolean triangle;
        private int triangleX;
        private int triangleY;
        */
        private Camera _camera;
        //private AlignmentState[] _states;
                
        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectAlignmentManager() 
        {
        }

        public static KinectAlignmentManager Instance
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
            internal static readonly KinectAlignmentManager instance = new KinectAlignmentManager();
        }
        #endregion
        
        /**
         * Arrange camera to good angle
         */
        public void Arrange()
        {
            if (!isChecking)
            {
                CameraPositioning camPos = new CameraPositioning();
                Thread camPosThread = new Thread(new ThreadStart(camPos.ArrangeCamera));
                camPosThread.Start();

                Thread.Sleep(10000);
                /*
                PrepareCamera();
                isChecking = true;

                KinectDataManager dataManager = KinectDataManager.Instance;
                dataManager.OnFixedUpdate += new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
                
                for(int i = -27; i < 27; i++)
                {
                    Console.WriteLine("angle " + i + " check: " + SetAngle(i));
                }
                 */

            }
            //TriggerAlignmentRequest();
        }

        void FixedUpdateHandler(object sender, DataManagerEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /**
         * Prepares camera if it hasn't been initialized
         */
        private Boolean PrepareCamera()
        {
            if (_camera == null)
            {
                _camera = KinectDataManager.Instance.Runtime.NuiCamera;
            }

            return (_camera != null);
        }

        public void checkAngle(int angle, SkeletonData skeletonData)
        {
            SetAngle(angle);
            /*
            // Check wether camera is there
            if (PrepareCamera() && skeletonData != null)
            {
                if (CheckTriangle(skeletonData))
                {
                    int tracked = JointAmount(skeletonData);
                    if (tracked > jointAmount)
                    {
                        bestAngle = angle;
                    }
                }
            }
            */

            /*
            KinectManager manager = KinectManager.Instance;
            Camera camera = manager.Runtime.NuiCamera;
            //manager.Runtime.SkeletonEngine.
            SetAngle(camera, 45);
             */
        }

        /**
         * Best angle for interactioj
         */
        public void SetBestAngle()
        {
            //PrepareCamera();
            //SetAngle(bestAngle);
        }


        public Boolean CheckTriangle(SkeletonData skeletonData)
        {
            return (skeletonData.Joints[JointID.HandLeft].TrackingState == JointTrackingState.Tracked) && (skeletonData.Joints[JointID.HandRight].TrackingState == JointTrackingState.Tracked) && (skeletonData.Joints[JointID.Head].TrackingState == JointTrackingState.Tracked);
        }

        public int JointAmount(SkeletonData skeletonData)
        {
            int tracked = 0;
            if (skeletonData.Joints[JointID.HandLeft].TrackingState == JointTrackingState.Tracked) 
            {
                tracked++;
            }
            if(skeletonData.Joints[JointID.HandRight].TrackingState == JointTrackingState.Tracked)
            {
                tracked++;   
            }
            if (skeletonData.Joints[JointID.Head].TrackingState == JointTrackingState.Tracked)
            {
                tracked++;
            }
            if (skeletonData.Joints[JointID.ElbowRight].TrackingState == JointTrackingState.Tracked)
            {
                tracked++;
            }
            if (skeletonData.Joints[JointID.ElbowLeft].TrackingState == JointTrackingState.Tracked)
            {
                tracked++;
            }
            if (skeletonData.Joints[JointID.HipCenter].TrackingState == JointTrackingState.Tracked)
            {
                tracked++;
            }

            Console.WriteLine("tracked: " + tracked);
            return tracked;
        }


        //Allows KinectManager to register on this event
        public event EventHandler OnAlignmentRequest;

        /**
         * Dispatched Event to KinectManager on AlignmentRequest
         */
        private void TriggerAlignmentRequest()
        {
            EventHandler handler = OnAlignmentRequest;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /**
         * Tries to set an angle
         */
        public Boolean SetAngle(int angle)
        {
            try
            {
                _camera.ElevationAngle = angle;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Angle couldn't be set: " + e.Message);
                return false;
            }
        }

    }
}
