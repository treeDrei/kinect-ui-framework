﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Research.Kinect.Nui;

using KinectControll.Manager.Input.Event;

using KinectControll.Manager.Data.Event;
using KinectControll.Manager.Data;

namespace KinectControll.Manager.Input
{
    public class KinectInputManager
    {
        private Boolean isStarted = false;
         
        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectInputManager() 
        {
            // Initial value outside visible area
            TriggerChanged(-100, -100);
        }

        void FixedUpdateHandler(object sender, DataManagerEventArgs e)
        {
            if (e.LeftHand != null && e.RightHand != null)
            {
                // Left hand is in front but currently not main hand
                if (e.LeftHand.Z > e.RightHand.Z && !isLeft)
                {
                    directionChangeIndex++;
                    if (directionChangeIndex >= directionChangeThreshold)
                    {
                        isLeft = true;
                        directionChangeIndex = 0;
                    }
                }
                // right hand in front but currently not main hand
                else if (e.LeftHand.Z < e.RightHand.Z && isLeft)
                {
                    directionChangeIndex++;
                    if (directionChangeIndex >= directionChangeThreshold)
                    {
                        isLeft = true;
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

        void UpdateHandler(object sender, DataManagerEventArgs e)
        {
            // Use values accoring to used hand
            if (isLeft && e.LeftHand != null)
            {
                SetInput(e.LeftHand.X, e.LeftHand.Y);
            }
            // Can't be updated if data is null
            else if(!isLeft && e.RightHand != null)
            {
                SetInput(e.RightHand.X, e.RightHand.Y);
            }
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

        public void SetInput(float handX, float handY)
        {
            double x = 0;
            double y = 0;

            
            y = height / 2 - (handY * height / 2);
            x = width / 2 - ((-1 * handX) * width / 2);

            x *= multiplyer;
            y *= multiplyer;


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
        
        public void SetInputJoints(Joint leftHand, Joint rightHand)
        {
            double x = 0;
            double y = 0;

            Joint joint = rightHand;
            // Check wich hand is further in front
            if (leftHand.Position.Z < rightHand.Position.Z)
            {
                joint = leftHand;
            }
 
            //x = ((rightHand.Position.X * width / 2) + width) * multiplyer;
            //y = ((rightHand.Position.Y * height / 2) + height) * multiplyer;
            //x = /*width - */((/*-1 * */rightHand.Position.X) * width);
            //y = (/*height - */(rightHand.Position.Y * height)) /* added */ * -1;

            y = height / 2 - (joint.Position.Y * height / 2);
            x = width / 2 - ((-1 * joint.Position.X) * width / 2);
            
            x *= multiplyer;
            y *= multiplyer;

            
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
