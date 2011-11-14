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

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.PoseView
{
    /// <summary>
    /// Interaktionslogik für ButtonsControl.xaml
    /// </summary>
    public partial class PoseControl : UserControl
    {
        public PoseControl()
        {
            InitializeComponent();
        }

        private void idleButton_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
