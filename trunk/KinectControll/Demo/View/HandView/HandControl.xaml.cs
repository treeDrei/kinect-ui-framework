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

// Required for double animation & Storyboard
using System.Windows.Media.Animation;

using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;
using KinectControll.Manager.Pose;
using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.HandView
{
    /// <summary>
    /// Interaktionslogik für HandControl.xaml
    /// </summary>
    public partial class HandControl : UserControl
    {
        public HandControl()
        {
            InitializeComponent();
        }
    }
}
