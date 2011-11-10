using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for thread manipulation
using System.Threading;
// Required to use camera
using Microsoft.Research.Kinect.Nui;
// Required for input stream
using KinectControll.Manager.Data;
// Required for camera data storage
using KinectControll.Model.Alignment;
// Required to move the camera
using KinectControll.Controller.Alignment;

namespace KinectControll.Manager.Alignment
{
    /**
     * Orients to: http://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx
     * Example: A process can have a user interface thread that manages interactions with 
     * the user and worker threads that perform other tasks while the user interface thread waits for user input.
     * 
     * This is a worker thread
     */
    public class CameraPositioning
    {
        // Displays thread state
        private Boolean _isRunning;
        // Camera for angle manipulation
        private Camera _camera;

        /**
         * This funtion is to be used as worker thread
         * It will allow higher performance in the other thread if they are spilt onto seperate processor cenrals
         */
        public void ArrangeCamera()
        {
            // Shows thread is running
            _isRunning = true;
            
            KinectDataManager dataManager = KinectDataManager.Instance;
            //dataManager.OnFixedUpdate += new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);

            for (int i = -21; i < 25; i += 2)
            {
                // Check wether an error occured or data is empty
                if (CameraController.SetAngle(i) && KinectDataManager.Instance.KinectDataVO != null)
                {
                    // Get all positions
                    Position head = KinectDataManager.Instance.KinectDataVO.Head;
                    Position leftHand = KinectDataManager.Instance.KinectDataVO.LeftHand;
                    Position rightHand = KinectDataManager.Instance.KinectDataVO.RightHand;

                    // Check wether all joints are stored
                    if (head != null && leftHand != null && rightHand != null)
                    {
                        // Store this state to model
                        AlignmentModel.Instance.AddCameraVO(i, new CameraVO(i, head, leftHand, rightHand));
                        Console.WriteLine("Angle " + i + ", h: " + head.Y + ", l: " + leftHand.Y + ", r: " + rightHand.Y);
                    }
                }

                // Wait, because motor is not as fast as cpu
                Thread.Sleep(600);
            }

            Console.WriteLine(AlignmentModel.Instance.CameraVOs.ToString());

            // Request data evaluation
            AlignmentController.AlignCamera();
            AlignmentController.AlignUser();

            // Shows thread complete
            _isRunning = false;
        }
    }
}
