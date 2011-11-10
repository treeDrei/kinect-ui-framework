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

using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.BackgroundView
{
    /// <summary>
    /// Interaktionslogik für BackgroundControl.xaml
    /// </summary>
    public partial class BackgroundControl : UserControl, IKinectView
    {
        public BackgroundControl()
        {
            InitializeComponent();
        }

        #region IKinectView interface implementation

        public const String SHOW = "show";
        public const String HIDE = "hide";
        private String state = SHOW;
        private IKinectView parent;

        /**
         * Changes view state
         */
        public void SetState(String state)
        {
            if (this.state != state)
            {
                if (state == SHOW)
                {
                    this.Show();
                }
                else if (state == HIDE)
                {
                    this.Hide();
                }
                this.state = state;
            }
        }

        /**
         * Sets parent element for this view
         */
        public void SetParent(IKinectView parent)
        {
            this.parent = parent;
        }

        /**
         * Returns Parent element to this view
         */
        public IKinectView GetParent()
        {
            return parent;
        }
        #endregion

        #region State change
        /**
         * Shows show animation
         */
        private void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        /**
         * Shows hide animation
         */
        private void Hide()
        {
            this.Visibility = Visibility.Hidden;
        }
        #endregion

        public void SetImage(String path)
        {
            Uri imageUri = new Uri(path);
            backgroundImage.Source = new BitmapImage(imageUri);
        }
    }
}
