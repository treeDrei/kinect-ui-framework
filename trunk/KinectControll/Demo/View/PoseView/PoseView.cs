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

using KinectControll.Manager.Pose;
using KinectControll.Model.Pose;

namespace KinectControll.Demo.View.PoseView
{
    public class PoseView : BaseKinectView
    {
        // Event allowing to register on it
        public event EventHandler OnNavigation;

        // List of buttons
        Dictionary<String, UIElement> icons;
        
        public PoseView()
            : base("HomePoseView", new PoseControl())
        {
            icons = new Dictionary<string, UIElement>();

            // Type cast (uses polymorphism) to concret type
            PoseControl homeControl = (PoseControl)userControl;

            //phoneButton = new ImageButton("phoneButton");
            CreateIcon(homeControl.homeButton, new HomePose());
            CreateIcon(homeControl.idleButton, new IdlePose());
        }

        #region Component Initialization
        /**
         * Creates a button
         */
        private void CreateIcon(Image icon, IPose pose)
        {
            PoseItem item = PoseModel.Instance.RegisterPose(pose);

            icons.Add(item.Pose.Name(), icon);
            icon.Opacity = 0;

            item.OnPoseBegin += new EventHandler(PoseBeginHandler);
            item.OnPoseComplete += new EventHandler(PoseEndHandler);
            item.OnPoseFailed += new EventHandler(PoseEndHandler);
        }
        #endregion

        #region Event Handling
        /**
         * Handles button selection event
         */
        void PoseBeginHandler(object sender, EventArgs e)
        {
            /// Sender is a Pose item
            PoseItem PoseItem = (PoseItem)sender;
            // Get image from icons list by its name
            Image icon = (Image)icons[PoseItem.Pose.Name()];

            // Animate selection
            DoubleAnimate(icon, 0, 1);
        }

        /**
         * Handles button deselection event
         */
        void PoseEndHandler(object sender, EventArgs e)
        {
            // Sender is a Pose item
            PoseItem PoseItem = (PoseItem)sender;
            // Get image from icons list by its name
            Image icon = (Image)icons[PoseItem.Pose.Name()];

            //ColorAnimate(button, Color.FromRgb(255, 255, 0), Color.FromRgb(0, 0, 0));
            DoubleAnimate(icon, 1, 0);
        }
        #endregion

        #region Animation
        /**
         * Animates size
         */
        private void DoubleAnimate(Image button, int from, int to)
        {
            // Double Animation generaates double values between from and two and applies them to a property of type double
            DoubleAnimation fadeAnimation = new DoubleAnimation();
            // 1 is completley visisble
            fadeAnimation.From = from;
            // 0 is hidden
            fadeAnimation.To = to;
            // Animation will take 300 miliseconds
            fadeAnimation.Duration = new Duration(TimeSpan.FromSeconds(.3));

            button.BeginAnimation(Image.OpacityProperty, fadeAnimation);
        }
        #endregion

    }
}
