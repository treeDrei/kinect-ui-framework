using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectControll.Manager.Item;
using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;

namespace KinectControll.Manager
{
    public class CollisionManager
    {
        KinectInputManager input;
        KinectItemManager items;

        #region Singleton instantiation
        /**
         * Singleton can't be instantiated from outside
         */
        private CollisionManager() 
        {
            input = KinectInputManager.Instance;
            items = KinectItemManager.Instance;
            input.OnChanged += new EventHandler<KinectInputManagerEventArgs>(controlChangedHandler);
        }

        public static CollisionManager Instance
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
            internal static readonly CollisionManager instance = new CollisionManager();
        }
        #endregion

        /**
         * Handles controll update
         */
        void controlChangedHandler(object sender, KinectInputManagerEventArgs e)
        {
            // Run throuhg enabled items. No point in checking the rest.
            items.Enabled.ForEach(delegate(KinectItem item)
            {    
                if (e.xPos < item.GetX() + item.GetWidth() && e.xPos > item.GetX())
                {
                    // Horizontal match
                    item.SetHit(e.yPos > item.GetY() && e.yPos < item.GetY() + item.GetHeight());
                }
                
            });
        }
    }
}
