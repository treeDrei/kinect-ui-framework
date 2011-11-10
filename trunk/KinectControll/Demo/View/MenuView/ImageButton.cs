using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using System.Windows.Media.Animation;
using System.Windows.Media;

namespace KinectControll.Demo.View.MenuView
{
    class ImageButton : Image
    {
        private String id;

        public ImageButton(String id)
        {
            this.id = id;
        }

        /**
         * Returns id
         */
        public String ID
        {
            get
            {
                return id;
            }
        }

        /**
         * Highlights button
         */
        public void Highlight(Boolean value)
        {
            if (value)
            {
                DoubleAnimate(100, 150);
            }
            else
            {
                DoubleAnimate(150, 100);
            }
        }

        /**
        * Animates size
        */
        private void DoubleAnimate(int from, int to)
        {
            DoubleAnimation positionAnimation = new DoubleAnimation();
            //positionAnimation.From = ;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(TimeSpan.FromSeconds(2));
            animation.AutoReverse = false;

            this.BeginAnimation(Image.WidthProperty, animation);
            this.BeginAnimation(Image.HeightProperty, animation);
        }
    }
}
