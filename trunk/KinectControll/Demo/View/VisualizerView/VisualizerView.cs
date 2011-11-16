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
using KinectControll.Model.Item;
using KinectControll.Model.Item.Selectable;

using KinectControll.Demo.View.MenuView.Event;

using KinectControll.Demo.View.BaseView;

// Required for rectangle
using System.Drawing;
using System.Windows.Forms;

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
            images = 36;
            CreateImages(images);

            buttons = new Dictionary<string, UIElement>();
            
            // Type cast (uses polymorphism) to concret type
            HomeControl visualizerControl = (HomeControl)userControl;

            CreateButton("leftButton", visualizerControl.leftButton);

            // Move button before using its psoition as reference
            double marginLeft = Screen.PrimaryScreen.Bounds.Width - visualizerControl.leftButton.Margin.Left - visualizerControl.rightButton.Width - 400;
            visualizerControl.rightButton.Margin = new Thickness(marginLeft, visualizerControl.rightButton.Margin.Top, 0, 0);

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
                String path = "/Images/";
                if (i < 9)
                {
                    path += "0";
                }
                path += (i + 1) + ".png";

                Console.WriteLine("Path: " + path);

                bitmapImage.UriSource = new Uri(path, UriKind.Relative);
                bitmapImage.EndInit();

                bitmapImages[i] = bitmapImage;
            }

            HomeControl visualizerControl = (HomeControl)userControl;
            visualizerControl.image1.Source = bitmapImages[4];
            visualizerControl.image1.Height = 897;
        }

        /**
         * Creates a button
         */
        private void CreateButton(String name, System.Windows.Controls.Image button)
        {
            KinectHoldable holdable = ButtonFactory.CreateHoldButton(name, button,3);
            buttons.Add(name, button);

            // Add event listeners
            holdable.OnSelection += new EventHandler(HoldHandler);

            KinectSelectable selectable = ButtonFactory.CreateSelectionButton(name, button, 3, holdable);
            selectable.OnSelection +=new EventHandler(SelectionHandler);
            selectable.OnDeselection += new EventHandler(DeselectionHandler);
                        
            // Adds item
            KinectItemManager items = KinectItemManager.Instance;
            items.RegisterItem(selectable);

            itemList.Add(selectable);
        }
        #endregion

        #region Event Handling
        /**
         * Handles button selection event
         */
        void HoldHandler(object sender, EventArgs e)
        {
            // Start rotation
            KinectHoldable selectable = (KinectHoldable)sender;
            // Retreive referenced object from buttons list
            System.Windows.Controls.Image button = (System.Windows.Controls.Image)buttons[selectable.GetID()];

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
       * Handles button selection event
       */
        private void SelectionHandler(object sender, EventArgs e)
        {
            // Sender is a Selectable
            KinectSelectable selectable = (KinectSelectable)sender;

            // Retreive referenced object from buttons list
            System.Windows.Controls.Image button = (System.Windows.Controls.Image)buttons[selectable.GetID()];
            // Animate selection
            ScaleAnimate(button, 100, 105);
        }

        /**
         * Handles button deselection event
         */
        private void DeselectionHandler(object sender, EventArgs e)
        {
            // Sender is a selectable
            KinectSelectable selectable = (KinectSelectable)sender;
            // Get image from buttons list by its id
            System.Windows.Controls.Image button = (System.Windows.Controls.Image)buttons[selectable.GetID()];

            ScaleAnimate(button, 105, 100);
        }
        #endregion

        #region Animation
        /**
         * Animates size
         */
        private void ScaleAnimate(System.Windows.Controls.Image button, int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            //positionAnimation.From = ;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(.3));
            animation.AutoReverse = false;

            button.BeginAnimation(System.Windows.Controls.Image.WidthProperty, animation);
            button.BeginAnimation(System.Windows.Controls.Image.HeightProperty, animation);
        }

        /**
         * Animates size
         */
        private void FadeAnimate(System.Windows.Controls.Image button, int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            //positionAnimation.From = ;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0));
            animation.AutoReverse = false;

            button.BeginAnimation(System.Windows.Controls.Image.OpacityProperty, animation);

        }
        #endregion

    }
}
