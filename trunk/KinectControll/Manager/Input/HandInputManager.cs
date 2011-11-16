using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Research.Kinect.Nui;

using KinectControll.Manager.Input.Event;

using KinectControll.Manager.Data.Event;
using KinectControll.Manager.Data;

using System.Windows.Input;

using KinectControll.Model.Alignment;
using KinectControll.Model.Input;

namespace KinectControll.Manager.Input
{
    public class HandInputManager: IManager
    {
        #region Private variables
        private Boolean isStarted = false;
        // Counts number of right
        private int directionChangeIndex = 0;
        private int directionChangeThreshold = 4;
        #endregion

        #region Singleton instantiation
        /**
         * Nested calss can only be called by ItemManager
         */
        private class SingletonCreator
        {
            /**
             * Nested class constructor will never be used
             */
            static SingletonCreator() { }

            /**
             * Instance will call private Singletor constructor
             */
            internal static readonly HandInputManager instance = new HandInputManager();
        }
        #endregion
        /**
         * Singleton shuldnt't be instantiated from outside
         */
        private HandInputManager() 
        {
            // Initial value outside visible area
            TriggerChanged(-100, -100);
        }

        /**
         * Handles fixed update event
         */
        private void FixedUpdateHandler(object sender, DataManagerEventArgs e)
        {
            InputModel model = InputModel.Instance;
            Boolean isLeft = InputModel.Instance.IsLeft;

            if (e.LeftHand != null && e.RightHand != null)
            {
                // Left hand is in front but currently not main hand
                if (e.LeftHand.Z < e.RightHand.Z && ! isLeft)
                {
                    directionChangeIndex++;
                    if (directionChangeIndex >= directionChangeThreshold)
                    {
                        isLeft = true;
                        directionChangeIndex = 0;
                    }
                }
                // right hand in front but currently not main hand
                else if (e.LeftHand.Z > e.RightHand.Z && isLeft)
                {
                    directionChangeIndex++;
                    if (directionChangeIndex >= directionChangeThreshold)
                    {
                        isLeft = false;
                        directionChangeIndex = 0;
                    }
                }
                // The current hand is in front
                else
                {
                    directionChangeIndex = 0;
                }
            }

            model.IsLeft = isLeft;
        }

        /**
         * Handles regular update event
         */
        private void UpdateHandler(object sender, DataManagerEventArgs e)
        {
            InputModel model = InputModel.Instance;
            double xValue = 0;
            double yValue = 0;
            // Use values accoring to used hand
            if (model.IsLeft && e.LeftHand != null)
            {
                xValue = e.LeftHand.X;
                yValue = e.LeftHand.Y;
            }
            // Can't be updated if data is null
            else if(!model.IsLeft && e.RightHand != null)
            {
                xValue = e.RightHand.X;
                yValue = e.RightHand.Y;
            }

            // Apply offste before usage
            xValue += (AlignmentModel.Instance.Offset.X);
            yValue += (AlignmentModel.Instance.Offset.Y);
            SetInput(xValue, yValue);

        }

        /**
         * Generates instance 
         */
        public static HandInputManager Instance
        {
            get
            {
                // Use nested class to create thread safe instance
                return SingletonCreator.instance;
            }
        }

        public void Start()
        {
            if (!isStarted)
            {
                isStarted = true;

                SDKDataManager dataManager = SDKDataManager.Instance;
                dataManager.OnUpdate += new EventHandler<DataManagerEventArgs>(UpdateHandler);
                dataManager.OnFixedUpdate += new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
            }
        }

        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;

                SDKDataManager dataManager = SDKDataManager.Instance;
                dataManager.OnUpdate -= new EventHandler<DataManagerEventArgs>(UpdateHandler);
                dataManager.OnFixedUpdate -= new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
            }
        }

        public void SetInput(double handX, double handY)
        {
            // Conrete x/y value on application
            double x = 0;
            double y = 0;

            InputModel inputModel = InputModel.Instance; 
            
            // Conversion from normateds value to 
            x = (inputModel.Multiplyer * .5 * handX + .5) * inputModel.Width;
            y = (inputModel.Multiplyer * -.5 * handY + .5) * inputModel.Height;

            if (x < 0)
            {
                x = 0;
            }
            else if (x > inputModel.Width)
            {
                x = inputModel.Width;
            }

            if (y < 0)
            {
                y = 0;
            }
            else if (y > inputModel.Height)
            {
                y = inputModel.Height;
            }

            // Triggers event TEST
            this.TriggerChanged(x, y);
        }

        #region Event
        // The Event will allow external objects to rigister on it
        public event EventHandler<HandInputManagerEventArgs> OnChanged;    

        /**
         * Event dispatch trigger
         */
        protected virtual void TriggerChanged(double x, double y)
        {
            InputModel.Instance.Current = new Point(x, y);
            // This copy will make it more thread-safe
            EventHandler<HandInputManagerEventArgs> handler = OnChanged;   
            if (handler != null)
            {
                var args = new HandInputManagerEventArgs() { xPos = x, yPos = y };  // vary
                handler(this, args);
            }
        }
        #endregion
    }
}
