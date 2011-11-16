using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Model.Item;
using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;

namespace KinectControll.Manager
{
    public class KinectCollisionManager: Manager, IManager
    {
        #region Private variables
        private KinectItemManager items;
        #endregion

        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private KinectCollisionManager() 
        {
            items = KinectItemManager.Instance;
        }

        public static KinectCollisionManager Instance
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
            internal static readonly KinectCollisionManager instance = new KinectCollisionManager();
        }
        #endregion

        #region IManager implementation
        /**
         * Stops data handling
         */
        public void Start()
        {
            if (!isStarted)
            {
                isStarted = true;
                HandInputManager.Instance.OnChanged += new EventHandler<HandInputManagerEventArgs>(controlChangedHandler);
            }
        }

        /**
         * Starts data handling
         */
        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;
                HandInputManager.Instance.OnChanged -= new EventHandler<HandInputManagerEventArgs>(controlChangedHandler);
            }
        }
        #endregion

        #region Event handling
        /**
         * Handles controll update
         */
        private void controlChangedHandler(object sender, HandInputManagerEventArgs e)
        {
            // Run throuhg enabled items. No point in checking the rest.
            items.Enabled.ForEach(delegate(KinectItem item)
            {
                if (e.xPos < item.GetX() + item.GetWidth() && e.xPos > item.GetX())
                {
                    // Horizontal match
                    item.SetHit(e.yPos > item.GetY() && e.yPos < item.GetY() + item.GetHeight());
                    //Console.WriteLine(item.GetID() + ": " + item.GetY() + ", " + item.GetHeight() + " - " + e.yPos + " | " + (e.yPos > item.GetY()) + " | " + (e.yPos < item.GetY() + item.GetHeight()) + " -> " + (e.yPos > item.GetY() && e.yPos < item.GetY() + item.GetHeight()));
                }
                else
                {
                    item.SetHit(false);
                }
                
            });
        }
        #endregion
    }
}
