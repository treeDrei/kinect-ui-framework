using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Animation;

using KinectControll.Manager;
using KinectControll.Manager.Item;
using KinectControll.Manager.Item.Selectable;

using KinectControll.Demo.View.MenuView.Event;

using KinectControll.Demo.View.BaseView;
// Required to use input manager
using KinectControll.Manager.Input;
// Required for Checks
using KinectControll.Check.Speed;
using KinectControll.Check.Move;
using KinectControll.Check.Distance;
// Required for swypable
using KinectControll.Manager.Item.Swypable;

namespace KinectControll.Demo.View.ImageView
{
    public class ImageView : BaseKinectView
    {
        private HandInputManager selection;
        private KinectItemManager itemManager;

        public ImageView()
            : base("ImageView", new ImageControl())
        {
            // Retreive selection Manager from kinect manager
            selection = HandInputManager.Instance;

            KinectItem item = new KinectItem("image1", 0, 0, 525, 525);

            SpeedCheck speedCheck = new SpeedCheck(100, 500);
            DistanceCheck distanceCheck = new DistanceCheck(100, 0);
            MoveCheck moveCheck = new MoveCheck(speedCheck, distanceCheck);

            KinectSwypeable swypeable = new KinectSwypeable(item, moveCheck);
            swypeable.OnSwypeComplete += new EventHandler(SwypeCompleteHandler);

            itemManager = KinectItemManager.Instance;
            itemManager.RegisterItem(swypeable);

            // Adds Kinectiten to list
            itemList.Add(swypeable);
        }
        
        #region Event Handling
        void SwypeCompleteHandler(object sender, EventArgs e)
        {
            ImageControl imageControl = (ImageControl)userControl;

            /*
            if (imageControl.demo1.Visibility == Visibility.Visible)
            {
                imageControl.demo1.Visibility = Visibility.Hidden;
            }
            else
            {
                imageControl.demo1.Visibility = Visibility.Visible;
            }
            */

            DoubleAnimate(imageControl.demo1, 0, -1920);
            //Canvas.SetLeft(demo1, -demo1.Width);
            Console.WriteLine("Swype guesture complete");
        }
        #endregion            
        
        #region Animation
        /**
         * Animates size
         */
        private void DoubleAnimate(Image image, int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(.5));
            animation.AutoReverse = false;

            image.BeginAnimation(Canvas.LeftProperty, animation);
        }
        #endregion

    }
}
