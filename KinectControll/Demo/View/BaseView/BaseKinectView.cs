using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Requred to use UserControl
using System.Windows.Controls;
// Requred for animations/storyboards
using System.Windows.Media.Animation;
// Reuired for duration
using System.Windows;
// Required foro kinect item handling
using KinectControll.Manager.Item;

namespace KinectControll.Demo.View.BaseView
{
    public class BaseKinectView
    {
        #region Public variables
        public const String SHOW = "show";
        public const String HIDE = "hide";
        #endregion

        #region Private variables
        // State will avoid double show/hide errors
        private String state = SHOW;
        private IKinectView parent;
        // Unique id allows view identification
        private String id;
        #endregion

        #region Protected  vairables
        // Child classes can edit storyboard if required
        // Storyboards for show/hide animation
        protected Storyboard showStoryboard;
        protected Storyboard hideStoryboard;
        // Basic view element to manipulate
        protected UserControl userControl;
        // List of Kinectitems to be activated/ deactivated
        protected List<KinectItem> itemList;
        #endregion

        /**
         * Constructor 
         */
        public BaseKinectView(String id, UserControl userControl = null)
        {
            this.id = id;

            // Creates a usercontrol if there is no default one to assign
            if (userControl == null)
            {
                userControl = new UserControl();
            }
            
            // Check wether name property has been set
            if (userControl.Name == "")
            {
                userControl.Name = id;
            }

            this.userControl = userControl;

            // Initialize animations on startup
            CreateHideStoryboard();
            CreateShowStoryboard();

            // Initaialize itemlist
            itemList = new List<KinectItem>();
        }

        #region Animation
        /**
        * Creates fade in and out animation
        */
        private void CreateShowStoryboard()
        {
            // Double Animation generaates double values between from and two and applies them to a property of type double
            DoubleAnimation fadeinAnimation = new DoubleAnimation();
            // 1 is completley visisble
            fadeinAnimation.From = 0;
            // 0 is hidden
            fadeinAnimation.To = 1;
            // Animation will take 300 miliseconds
            fadeinAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            // Creates storyboard
            showStoryboard = new Storyboard();
            showStoryboard.Children.Add(fadeinAnimation);

        
            // Links storyboard to hand image to allow manipulation
            Storyboard.SetTargetName(fadeinAnimation, userControl.Name);
            // Links property to allow animation
            Storyboard.SetTargetProperty(fadeinAnimation, new PropertyPath(UserControl.OpacityProperty));
          }

        /**
        * Creates fade in and out animation
        */
        private void CreateHideStoryboard()
        {
            // Double Animation generaates double values between from and two and applies them to a property of type double
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            // 1 is completley visisble
            fadeOutAnimation.From = 1;
            // 0 is hidden
            fadeOutAnimation.To = 0;
            // Animation will take 300 miliseconds
            fadeOutAnimation.Duration = new Duration(TimeSpan.FromSeconds(.5));

            // Creates storyboard
            hideStoryboard = new Storyboard();
            hideStoryboard.Children.Add(fadeOutAnimation);


            // Links storyboard to hand image to allow manipulation
            Storyboard.SetTargetName(fadeOutAnimation, userControl.Name);
            // Links property to allow animation
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(UserControl.OpacityProperty));
        }
        #endregion

        #region Setter Methods
        /**
         * Sets parent element for this view
         */
        public void SetParent(IKinectView parent)
        {
            this.parent = parent;
        }
        #endregion

        #region Getter Methods
        /**
         * Returns Parent element to this view
         */
        public IKinectView GetParent()
        {
            return parent;
        }

        /**
         * 
         */
        public UserControl GetUSerControl()
        {
            return userControl;
        }
        #endregion

        #region State change
        /**
         * Changes view state
         */
        public void SetState(String state)
        {
            // Check wether a state change has occured
            if (this.state != state)
            {
                this.state = state;

                // Act according to new state
                switch (state)
                {
                    case SHOW:
                        this.Show();
                        break;
                    case HIDE:
                        this.Hide();
                        break;
                    default:
                        Console.WriteLine("Wrong state input");
                        break;
                }
            }
        }
        
        // C# is quite strict about polymorphism. Functions have to be either virtual, abstract or interface implementations to override existing functions
        // A regular public function can't be overritten 
            
        /**
         * Shows show animation
         * This function is virtual. It can be overritten.
         */
        protected virtual void Show()
        {
            showStoryboard.Begin(userControl);
            itemList.ForEach(
                delegate(KinectItem item)
                {
                    // Enables all Kinect items on this view
                    item.SetEnabled(true);
                }
            );
        }

        /**
         * Shows hide animation
         * This function is virtual. It can be overritten.
         */
        protected virtual void Hide()
        {
            hideStoryboard.Begin(userControl);
            itemList.ForEach(
                delegate(KinectItem item)
                {
                    // Disables all Kinect items on this view
                    item.SetEnabled(false);
                }
            );
        }
        #endregion

    }
}
