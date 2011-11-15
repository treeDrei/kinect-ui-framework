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

using KinectControll.Demo.Factory;

using KinectControll.Manager;
using KinectControll.Manager.Item;
using KinectControll.Manager.Item.Selectable;

using KinectControll.Demo.View.MenuView.Event;

using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.VisualizerView
{
    public class VisualizerView : BaseKinectView
    {
        // List of buttons
        private Dictionary<String, UIElement> buttons;
        private BitmapImage[] bitmapImages;
        // Current image index
        private int index;
        private int images;
        
        public VisualizerView()
            : base("VisualizerView", new HomeControl())
        {
            images = 23;
            CreateImages(images);

            buttons = new Dictionary<string, UIElement>();
            
            // Type cast (uses polymorphism) to concret type
            HomeControl visualizerControl = (HomeControl)userControl;

            CreateButton("leftButton", visualizerControl.leftButton);
            CreateButton("rightButton", visualizerControl.rightButton);
        }

        #region Component Initialization
        private void CreateImages(int elements)
        {
            bitmapImages = new BitmapImage[elements];
            for (int i = 0; i < elements; i++)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri("/Images/360_"+i+".png", UriKind.Relative);
                bitmapImage.EndInit();

                bitmapImages[i] = bitmapImage;
            }

            HomeControl visualizerControl = (HomeControl)userControl;
            visualizerControl.image1.Source = bitmapImages[4];
        }

        /**
         * Creates a button
         */
        private void CreateButton(String name, Image button)
        {
            KinectHoldable holdable = ButtonFactory.CreateHoldButton(name, button,3);
            buttons.Add(name, button);

            // Add event listeners
            holdable.OnSelection += new EventHandler(SelectionHandler);
            //holdable.OnDeselection += new EventHandler(ButtonDeselectedHandler);
                        
            // Adds item
            KinectItemManager items = KinectItemManager.Instance;
            items.RegisterItem(holdable);

            itemList.Add(holdable);
        }
        #endregion

        #region Event Handling
        /**
         * Handles button selection event
         */
        void SelectionHandler(object sender, EventArgs e)
        {
            // Start rotation
            KinectHoldable selectable = (KinectHoldable)sender;
            // Retreive referenced object from buttons list
            Image button = (Image)buttons[selectable.GetID()];

            HomeControl visualizerControl = (HomeControl)userControl;
            
            if (button == visualizerControl.leftButton)
            {
                index++;
            }
            else if (button == visualizerControl.rightButton)
            {
                index--;
            }

            // Modulo for rotation
            index = index % images;
            if (index < 0)
            {
                index = images + index;
            }

            visualizerControl.image1.Source = bitmapImages[index];


            /*
            // Animate selection
            FadeAnimate(button, 70, 111);
            */
        }

        /**
         * Handles button deselection event
         *
        void ButtonDeselectedHandler(object sender, EventArgs e)
        {
            // Stop rotation
        }
         */
        #endregion

        #region Animation
        /**
         * Animates size
         */
        private void FadeAnimate(Image button, int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            //positionAnimation.From = ;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0));
            animation.AutoReverse = false;

            button.BeginAnimation(Image.OpacityProperty, animation);

        }
        #endregion

    }
}
