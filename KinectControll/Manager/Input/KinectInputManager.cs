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

namespace KinectControll.Manager.Input
{
    public class KinectInputManager
    {
        private Boolean isStarted = false;
         
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
            internal static readonly KinectInputManager instance = new KinectInputManager();
        }
        #endregion
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectInputManager() 
        {
            // Initial value outside visible area
            TriggerChanged(-100, -100);
        }

        /**
         * Handles fixed update event
         */
        private void FixedUpdateHandler(object sender, DataManagerEventArgs e)
        {
            if (e.LeftHand != null && e.RightHand != null)
            {
                // Left hand is in front but currently not main hand
                if (e.LeftHand.Z < e.RightHand.Z && !isLeft)
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
        }

        /**
         * Handles regular update event
         */
        private void UpdateHandler(object sender, DataManagerEventArgs e)
        {
            double xValue = 0;
            double yValue = 0;
            // Use values accoring to used hand
            if (isLeft && e.LeftHand != null)
            {
                xValue = e.LeftHand.X;
                yValue = e.LeftHand.Y;
            }
            // Can't be updated if data is null
            else if(!isLeft && e.RightHand != null)
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
        public static KinectInputManager Instance
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

                KinectDataManager dataManager = KinectDataManager.Instance;
                dataManager.OnUpdate += new EventHandler<DataManagerEventArgs>(UpdateHandler);
                dataManager.OnFixedUpdate += new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
            }
        }

        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;

                KinectDataManager dataManager = KinectDataManager.Instance;
                dataManager.OnUpdate -= new EventHandler<DataManagerEventArgs>(UpdateHandler);
                dataManager.OnFixedUpdate -= new EventHandler<DataManagerEventArgs>(FixedUpdateHandler);
            }
        }

        // Debug info will be dispatched if debug is enabled
        
        // Width and hight are beeing used for manipulation calculation
        private double width = 500;
        private double height = 500;

        // Counts number of right
        private int directionChangeIndex = 0;
        private int directionChangeThreshold = 4;
        private Boolean isLeft = true;

        // Multiply user input to enhance distance with movement
        private double multiplyer = 1;

        private Point point;

        public void SetInput(double handX, double handY)
        {
            // Conrete x/y value on application
            double x = 0;
            double y = 0;

            // Conversion from normateds value to 
            x = (multiplyer * .5 * handX + .5) * width;
            y = (multiplyer * - .5 * handY + .5) * height;

            if (x < 0)
            {
                x = 0;
            }
            else if (x > width)
            {
                x = width;
            }

            if (y < 0)
            {
                y = 0;
            }
            else if (y > width)
            {
                y = height;
            }

            // Triggers event TEST
            this.TriggerChanged(x, y);
        }

        #region Event
        // The Event will allow external objects to rigister on it
        public event EventHandler<KinectInputManagerEventArgs> OnChanged;    

        /**
         * Event dispatch trigger
         */
        protected virtual void TriggerChanged(double x, double y)
        {
            point = new Point(x, y);
            // This copy will make it more thread-safe
            EventHandler<KinectInputManagerEventArgs> handler = OnChanged;   
            if (handler != null)
            {
                var args = new KinectInputManagerEventArgs() { xPos = x, yPos = y };  // vary
                handler(this, args);
            }
        }
        #endregion

        /**
         * Current selection 
         */
        public Point Current
        {
            get
            {
                return point;
            }
        }

        /**
         * Height getter
         */
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }


        /**
         * Height set & get
         */
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        /**
         * Height set & get
         */
        public double Multiplyer
        {
            get
            {
                return multiplyer;
            }
            set
            {
                multiplyer = value;
            }
        }
    }

    

}
