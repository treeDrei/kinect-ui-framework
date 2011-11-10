using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use Image class
using System.Windows.Controls;

using KinectControll.Demo.View.BaseView;

// Required for double animation & Storyboard
using System.Windows.Media.Animation;

using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;
using KinectControll.Manager.Pose;

using System.Windows;

namespace KinectControll.Demo.View.HandView
{
    public class HandView : BaseKinectView
    {
        private Storyboard handFadeInStoryboard;
        private Storyboard handFadeOutStoryboard;

        public HandView()
            : base("HandView", new HandControl())
        {
            KinectInputManager inputManager = KinectInputManager.Instance;
            inputManager.OnChanged += new EventHandler<KinectInputManagerEventArgs>(ControlChangedHandler);

            KinectPoseManager poseManager = KinectPoseManager.Instance;
            KinectPoseItem idleItem = poseManager.RegisterPose(new Idle());
            idleItem.OnPoseBegin += new EventHandler(IdleBeginHandler);
            idleItem.OnPoseComplete += new EventHandler(IdleEndHandler);
            idleItem.OnPoseFailed += new EventHandler(IdleEndHandler);

            // Initialize animations for usage later on
            CreateStoryBoard();
        }

        /**
         * Creates fade in and out animation
         */
        private void CreateStoryBoard()
        {
            // Double Animation generaates double values between from and two and applies them to a property of type double
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            // 1 is completley visisble
            fadeOutAnimation.From = 1;
            // 0 is hidden
            fadeOutAnimation.To = 0;
            // Animation will take 300 miliseconds
            fadeOutAnimation.Duration = new Duration(TimeSpan.FromSeconds(.3));

            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(.3));

            // Creates storyboard
            handFadeInStoryboard = new Storyboard();
            handFadeInStoryboard.Children.Add(fadeInAnimation);

            handFadeOutStoryboard = new Storyboard();
            handFadeOutStoryboard.Children.Add(fadeOutAnimation);

            HandControl handControl = (HandControl)userControl;

            // Links storyboard to hand image to allow manipulation
            Storyboard.SetTargetName(fadeOutAnimation, handControl.hand.Name);
            Storyboard.SetTargetName(fadeInAnimation, handControl.hand.Name);
            // Links property to allow animation
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(Image.OpacityProperty));
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(Image.OpacityProperty));
        }


        #region Event Handling

        /**
         * Show hand symbol on idle pose end
         */
        void IdleEndHandler(object sender, EventArgs e)
        {
            HandControl handControl = (HandControl)userControl;
            // Begins animation assigned to object on storyboard
            handFadeInStoryboard.Begin(handControl.hand);
        }

        /**
         * Hide hand symbil on idle pose start
         */
        private void IdleBeginHandler(object sender, EventArgs e)
        {
            HandControl handControl = (HandControl)userControl;
            // Begins animation assigned to object on storyboard
            handFadeOutStoryboard.Begin(handControl.hand);
        }

        /**
         * Updates hand position on view
         */
        void ControlChangedHandler(object sender, KinectInputManagerEventArgs e)
        {
            // Type cast to conrete type
            HandControl handControl = (HandControl)userControl;

            Canvas.SetTop(handControl.hand, e.yPos);
            Canvas.SetLeft(handControl.hand, e.xPos);
        }

        #endregion

    }
}
