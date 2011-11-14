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

namespace KinectControll.Demo.View.HomeView
{
    public class HomeView : BaseKinectView
    {
        // List of buttons
        Dictionary<String, UIElement> buttons;
        
        // Event allowing to register on it
        public event EventHandler OnNavigation;
        
        public HomeView()
            : base("HomeView", new HomeControl())
        {
            buttons = new Dictionary<string, UIElement>();
            
            // Type cast (uses polymorphism) to concret type
            HomeControl homeControl = (HomeControl)userControl;

            //phoneButton = new ImageButton("phoneButton");
            CreateButton("homeButton", homeControl.homeButton);   
        }

        #region Component Initialization
        /**
         * Creates a button
         */
        private void CreateButton(String name, Image button)
        {
            // Button1 item
            KinectItem item = new KinectItem(name, button.Margin.Left, button.Margin.Top, button.Width, button.Height);
            buttons.Add(name, button);

            KinectSelectable selection = new KinectSelectable(item, 10);
            // Add event listeners
            selection.OnSelection += new EventHandler(SelectionHandler);
            selection.OnDeselection += new EventHandler(ButtonDeselectedHandler);

            // Navigation will happen later then selection
            KinectSelectable navigation = new KinectSelectable(selection, 20);
            navigation.OnSelection += new EventHandler(NavigationHandler);

            // Adds item
            KinectItemManager items = KinectItemManager.Instance;
            items.RegisterItem(navigation);

            itemList.Add(selection);
        }
        #endregion

        #region Event Trigger
        /**
        * Event dispatch trigger
        * Fill with EventArgs.Empty if nothing is beeing attached
        */
        protected virtual void TriggerNavigation(MenuEventArgs e)    // the Trigger
        {
            EventHandler handler = OnNavigation;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion


        #region Event Handling
        void NavigationHandler(object sender, EventArgs e)
        {
            // Sender is a Selectable
            KinectSelectable selectable = (KinectSelectable)sender;

            // Argumaents will tell wich button has been selected
            MenuEventArgs args = new MenuEventArgs();
            // This is a test
            args.ViewID = "Test";
            // Triggers event
            TriggerNavigation(args);
        }

        /**
         * Handles button selection event
         */
        void SelectionHandler(object sender, EventArgs e)
        {
            // Sender is a Selectable
            KinectSelectable selectable = (KinectSelectable)sender;
            
            // Retreive referenced object from buttons list
            Image button = (Image)buttons[selectable.GetID()];
            // Animate selection
            DoubleAnimate(button, 70, 111);
            // Debug output
            Console.WriteLine(selectable.GetID() + " selected");
        }

        /**
         * Handles button deselection event
         */
        void ButtonDeselectedHandler(object sender, EventArgs e)
        {
            // Sender is a selectable
            KinectSelectable selectable = (KinectSelectable)sender;
            // Get image from buttons list by its id
            Image button = (Image)buttons[selectable.GetID()];

            //ColorAnimate(button, Color.FromRgb(255, 255, 0), Color.FromRgb(0, 0, 0));
            DoubleAnimate(button, 111, 70);
            // Debug output
            Console.WriteLine(selectable.GetID() + " deselected");
        }
        #endregion

        #region Animation
        /**
         * Animates size
         */
        private void DoubleAnimate(Image button, int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            //positionAnimation.From = ;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(.5));
            animation.AutoReverse = false;

            button.BeginAnimation(Image.WidthProperty, animation);
            button.BeginAnimation(Image.HeightProperty, animation);
        }
        #endregion

    }
}
