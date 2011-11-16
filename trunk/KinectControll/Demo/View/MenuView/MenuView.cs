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
using KinectControll.Model.Item;
using KinectControll.Model.Item.Selectable;

using KinectControll.Demo.View.MenuView.Event;

using KinectControll.Demo.View.BaseView;

using KinectControll.Demo.Factory;

namespace KinectControll.Demo.View.MenuView
{
    public class MenuView : BaseKinectView
    {
        // List of buttons
        private Dictionary<String, UIElement> buttons;
        
        // Event allowing to register on it
        public event EventHandler OnVisualizerNavigation;
        public event EventHandler OnImageNavigation;
        public event EventHandler OnClose;

        private const String IMAGE_NAME = "imageButton";
        private const String VISUALIZER_NAME = "visualizerButton";
        private const String CLOSE_NAME = "closeButton";

        public MenuView()
            : base("MenuView", new MenuControl())
        {
            buttons = new Dictionary<string, UIElement>();
            
            // Type cast (uses polymorphism) to concret type
            MenuControl menuControl = (MenuControl)userControl;

            //phoneButton = new ImageButton("phoneButton");
            CreateMenuButton(IMAGE_NAME, menuControl.imageButton);
            //questionButton = new ImageButton("questionButton");
            CreateMenuButton(VISUALIZER_NAME, menuControl.visualizerButton);

            CreateCloseButton();
        }

        #region Component Initialization

        /**
         * Creates a close button
         */
        private void CreateCloseButton()
        {
            MenuControl menuControl = (MenuControl)userControl;
    
            KinectSelectable selection = ButtonFactory.CreateSelectionButton("closeButton", menuControl.closeButton, 5);
            buttons.Add(selection.GetID(), menuControl.closeButton);

            // Add event listeners
            selection.OnSelection += new EventHandler(CloseSelectionHandler);
            selection.OnDeselection += new EventHandler(CloseDeselectionHandler);

            // Navigation will happen later then selection
            KinectSelectable close = new KinectSelectable(selection, 20);
            close.OnSelection += new EventHandler(NavigationHandler);

            // Adds item
            KinectItemManager items = KinectItemManager.Instance;
            items.RegisterItem(close);

            itemList.Add(close);
        }
        
        /**
         * Creates a button
         */
        private void CreateMenuButton(String name, Image button)
        {
            // Button1 item
            KinectItem item = new KinectItem(name, button.Margin.Left, button.Margin.Top, button.Width, button.Height);
            buttons.Add(name, button);

            KinectSelectable selection = new KinectSelectable(item, 5);
            // Add event listeners
            selection.OnSelection += new EventHandler(SelectionHandler);
            selection.OnDeselection += new EventHandler(DeselectionHandler);

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
        protected virtual void TriggerVisualizerNavigation()    // the Trigger
        {
            EventHandler handler = OnVisualizerNavigation;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void TriggerImageNavigation()    // the Trigger
        {
            EventHandler handler = OnImageNavigation;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void TriggerClose()    // the Trigger
        {
            EventHandler handler = OnClose;   // make a copy to be more thread-safe
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        #endregion


        #region Event Handling
        void NavigationHandler(object sender, EventArgs e)
        {
            // Sender is a Selectable
            KinectSelectable selectable = (KinectSelectable)sender;

            switch (selectable.GetID())
            {
                case IMAGE_NAME:
                    TriggerImageNavigation();
                    break;
                case VISUALIZER_NAME:
                    TriggerVisualizerNavigation();
                    break;
                case CLOSE_NAME:
                    TriggerClose();
                    break;
            }
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
            DoubleAnimate(button, 105, 111);
        }

        /**
         * Handles button deselection event
         * Close animation differs from menu animation
         */
        void DeselectionHandler(object sender, EventArgs e)
        {
            // Sender is a selectable
            KinectSelectable selectable = (KinectSelectable)sender;
            // Get image from buttons list by its id
            Image button = (Image)buttons[selectable.GetID()];

            //ColorAnimate(button, Color.FromRgb(255, 255, 0), Color.FromRgb(0, 0, 0));
            DoubleAnimate(button, 111, 105);
        }

        /**
         * Handles button selection event
         */
        void CloseSelectionHandler(object sender, EventArgs e)
        {
            // Sender is a Selectable
            KinectSelectable selectable = (KinectSelectable)sender;

            // Retreive referenced object from buttons list
            Image button = (Image)buttons[selectable.GetID()];
            // Animate selection
            DoubleAnimate(button, 70, 75);
        }

        /**
         * Handles button deselection event
         */
        void CloseDeselectionHandler(object sender, EventArgs e)
        {
            // Sender is a selectable
            KinectSelectable selectable = (KinectSelectable)sender;
            // Get image from buttons list by its id
            Image button = (Image)buttons[selectable.GetID()];

            DoubleAnimate(button, 75, 70);
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
            animation.Duration = new Duration(TimeSpan.FromSeconds(2));
            animation.AutoReverse = false;

            button.BeginAnimation(Image.WidthProperty, animation);
            button.BeginAnimation(Image.HeightProperty, animation);
        }
        #endregion

    }
}
