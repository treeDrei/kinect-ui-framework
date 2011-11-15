using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for Point
using System.Windows;

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

using KinectControll.Manager.Input;

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
        // Best camera angle found (default: streight ahead) 
        private int _bestAngle = 0;

        private PositionVector _head;
        private PositionVector _leftHand;
        private PositionVector _rightHand;

        private PositionModel _model;


        /**
         * This funtion is to be used as worker thread
         * It will allow higher performance in the other thread if they are spilt onto seperate processor cenrals
         */
        public void Run()
        {
            _isRunning = true;

            // Makes sure hand input will not cause selection
            HandInputManager.Instance.Stop();

            while(!CameraController.SetAngle(27))

            _model = PositionModel.Instance;

            CollectBetterData();
            EvaluateBetterAngle();

            EvaluateUserPosition();

            // Thread is complete. Start input
            HandInputManager.Instance.Start();

            _isRunning = false;
        }

        /**
         * Stop this thread
         */
        public void Stop()
        {
            if (_isRunning)
            {
                // Input is required again
                HandInputManager.Instance.Start();
                Thread.CurrentThread.Abort();
            }
        }

        /**
         * Finds best offset for user on angle
         */
        private void EvaluateUserPosition()
        {
            CameraVO vo = AlignmentModel.Instance.GetCameraVO(_bestAngle);
            //AlignmentModel.Instance.Offset = new Point(-vo.Head.X*1.5,0);
        }

        /**
        * Finds best angle and sets it
        */
        private void EvaluateBetterAngle()
        {
            Dictionary<int, CameraVO> dict = AlignmentModel.Instance.CameraVOs;
           
            int bestAngle = FindBestAngle(dict);
            // Store camera vo during next process
            CameraVO bestVO = dict[bestAngle];

            // Remove best angle to avoid it from beeing found
            dict.Remove(bestAngle);
            int secondBestAngle = FindBestAngle(dict);
       
            // Everything back to normal
            dict.Add(bestAngle, bestVO);

            _bestAngle = FindAngleBetween(bestVO, dict[secondBestAngle]);
            // Store this angle
            StoreAngle(_bestAngle, 10100);
        }

        /**
         * Will retreive good data for angle and stores it
         */
        private Boolean StoreAngle(int angle, int threshold = 100)
        {
            while (!CameraController.SetAngle(angle))
            {
                // Try after delay
                Thread.Sleep(300);    
            }

            int count = 0;
            while (WaitForGoodData())
            {
                Thread.Sleep(30);
                if (count > threshold)
                {
                    // Does not store this data because nothing is coming
                    return false;
                }
                count++;

                // Wait
            }

            // Store this state to model
            AlignmentModel.Instance.RegisterCameraVO(angle, new CameraVO(angle, _head, _leftHand, _rightHand));
            return true;

        }

        /**
         * Finds best angle between two angles
         */
        private int FindAngleBetween(CameraVO bestVO, CameraVO secondBestVO)
        {
            int difference = bestVO.Angle + secondBestVO.Angle;
            double bestOffset = (FindYOffset(bestVO));

            // Calculation with 0 would cause error
            if(bestOffset == 0)
            {
                // Angle is already best possible angle
                return bestVO.Angle;
            }
            return (int)Math.Round((double)(bestOffset / (FindYOffset(secondBestVO)) * difference));

        }

        private double FindYOffset(CameraVO vo)
        {
            // Middle value between both hands
            double MiddleValue = (vo.LeftHand.Y + vo.RightHand.Y) / 2;
            // Returns offset value between triangle center and 0/0 coordinate
            return  Math.Abs(vo.Head.Y + MiddleValue);
        }

        private Boolean WithoutBest(int angle, CameraVO vo)
        {
            return (angle != _bestAngle);
        }

        /**
         * Returns best angle in a dictionary
         */
        private int FindBestAngle(Dictionary<int, CameraVO> dict)
        {
            // best offset is initially so high any offset will be better
            double bestOffset = 100;
            int bestAngle = 0;

            foreach (KeyValuePair<int, CameraVO> keyValuePair in dict)
            {
                // Compare wether both hands are located at approximatley the same height
                double compare = keyValuePair.Value.LeftHand.Y - keyValuePair.Value.RightHand.Y;
                if (compare < 0.1 && compare > -0.1)
                {
                    // Middle value between both hands
                    double MiddleValue = (keyValuePair.Value.LeftHand.Y + keyValuePair.Value.RightHand.Y) / 2;

                    // Store distance between hand middle value and head 
                    double compareOffset = Math.Abs(keyValuePair.Value.Head.Y + MiddleValue);

                    // Check wether the new offset has a value closer to 0 then the current best
                    if (compareOffset < bestOffset)
                    {
                        // offset will be compared to current value
                        bestOffset = compareOffset;
                        // Current angle is the best
                        bestAngle = keyValuePair.Key;
                    }

                }
            }

            return bestAngle;
        }


        /**
         * Checks wether both hands are at approxemitley the same height
         */
        private Boolean BothHandsAtSameHight(double leftY, double rightY, double offset = 0.1)
        {
            // Distance is always positive
            double compare = Math.Abs(leftY - rightY);
            // Compare wether both hands are located at approximatley the same height
            return (compare < offset );
        }

        private void CollectBetterData()
        {
            int compCount = 0;
            for (int i = -25; i < 26; i += 10)
            {
                if (StoreAngle(i))
                {
                    compCount++;
                }
            }

            // There should be at least two data sets to continue
            if (compCount < 2)
            {
                CollectBetterData();
            }
        }

        /**
         * Returns false if this is good data
         */
        private Boolean WaitForGoodData()
        {
            _model = PositionModel.Instance;
            // Data exists
            if (_model.Normed != null)
            {
                // Get all positions
                _head = _model.Normed.Head;
                _leftHand = _model.Normed.LeftHand;
                _rightHand = _model.Normed.RightHand;

                // Check wether all joints have stored data
                if (_head != null && _leftHand != null && _rightHand != null)
                {
                    // if both hands are at same height use this data
                    return !BothHandsAtSameHight(_leftHand.Y, _rightHand.Y);
                }
            }

            // We need to wait
            return true;
        }
    }
}