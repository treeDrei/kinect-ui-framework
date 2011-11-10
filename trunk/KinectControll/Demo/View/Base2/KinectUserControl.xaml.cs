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

namespace KinectControll.Demo.View.Base
{
    /// <summary>
    /// Interaktionslogik für KinectControl.xaml
    /// </summary>
    public abstract class KinectUserControl : UserControl, IKinectView
    {
        public static String SHOW = "show";
        public static String HIDE = "hide";

        public IKinectView parent;

        //protected UIElement parent;

        public KinectUserControl()
        {
            InitializeComponent();
            //this.parent = parent;
        }

        /**
         * Will set view state. Show or Hide can't be called twice this way.
         */
        public void SetState()
        {

        }

        /**
         * Show animation has to be implemented
         */
        protected abstract void Show();

        /**
         * Hide animation has to be implemented
         */
        protected abstract void Hide();

        /**
         * Sets parenet element
         */
        public void SetParent(IKinectView parent)
        {
            this.parent = parent;
        }

        public IKinectView GetParent()
        {
            return parent;
        }

    }
}
