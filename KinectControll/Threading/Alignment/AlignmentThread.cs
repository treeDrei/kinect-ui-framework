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
//
using KinectControll.Model.Position;

namespace KinectControll.Threading.Alignment
{
    /**
     * Orients to: http://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx
     * Example: A process can have a user interface thread that manages interactions with 
     * the user and worker threads that perform other tasks while the user interface thread waits for user input.
     * 
     * This is a worker thread
     */
    public class AlignmentThread
    {
        // Displays thread state
        private Boolean _isRunning;
        // Camera for angle manipulation
        private Camera _camera;

        // Best camera angle found (default: streight ahead) 
        private int _bestAngle = 0;


        /**
         * This funtion is to be used as worker thread
         * It will allow higher performance in the other thread if they are spilt onto seperate processor cenrals
         */
        public void Run()
        {
            _isRunning = true;

            CollectCameraData();

            EvaluateCameraAngle();
            // Make sure camera can be mooved again
            Thread.Sleep(500);
            CameraController.SetAngle(_bestAngle);

            EvaluateUserPosition();
            
            
            _isRunning = false;
        }

        public void Stop()
        {
            if (_isRunning)
            {
                Thread.CurrentThread.Abort();
            }
        }

        /**
         * Finds best offset for user on angle
         */
        private void EvaluateUserPosition()
        {
            CameraVO vo = AlignmentModel.Instance.GetCameraVO(_bestAngle);
            AlignmentModel.Instance.Offset = new System.Windows.Point(-vo.Head.X,0);
        }

        /**
         * Returns best angle
         */
        private void EvaluateCameraAngle()
        {
            Dictionary<int, CameraVO> dict = AlignmentModel.Instance.CameraVOs;
            //Dictionary<int, float> offsets = new Dictionary<int,float>();

            // best offset is initially so high any offset will be better
            double bestOffset = 100;

            foreach(KeyValuePair<int, CameraVO> keyValuePair in dict)
            {
                // Compare wether both hands are located at approximatley the same height
                double compare = keyValuePair.Value.LeftHand.Y - keyValuePair.Value.RightHand.Y;
                if(compare < 0.1 && compare > -0.1)
                {
                    // Middle value between both hands
                    double MiddleValue = (keyValuePair.Value.LeftHand.Y + keyValuePair.Value.RightHand.Y)/2;
                    
                    // Store distance between hand middle value and head 
                    double compareOffset =  Math.Abs(keyValuePair.Value.Head.Y + MiddleValue);

                    // Check wether the new offset has a value closer to 0 then the current best
                    if (compareOffset < bestOffset)
                    {
                        // offset will be compared to current value
                        bestOffset = compareOffset;
                        // Current angle is the best
                        _bestAngle = keyValuePair.Key;
                    }

                }
            }
       }

        /**
         * Collects angle data to evaluate wich angle was best
         */
        private void CollectCameraData()
        {            
            for (int i = -21; i < 25; i += 2)
            {
                PositionModel model = PositionModel.Instance;

                // Check wether an error occured or data is empty
                if (CameraController.SetAngle(i) && model.Normed != null)
                {
                    // Get all positions
                    PositionVector head = model.Normed.Head;
                    PositionVector leftHand = model.Normed.LeftHand;
                    PositionVector rightHand = model.Normed.RightHand;

                    // Check wether all joints are stored
                    if (head != null && leftHand != null && rightHand != null)
                    {
                        // Store this state to model
                        AlignmentModel.Instance.SetCameraVO(i, new CameraVO(i, head, leftHand, rightHand));
                        Console.WriteLine("Angle " + i + ", h: " + head.Y + ", l: " + leftHand.Y + ", r: " + rightHand.Y);
                    }
                }

                // Wait, because motor is not as fast as cpu
                Thread.Sleep(600);
            }

            Console.WriteLine(AlignmentModel.Instance.CameraVOs.ToString());
        }
    }
}
