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

using KinectControll.Model.Item;
using KinectControll.Model.Item.Swypable;
using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;

// Check
using KinectControll.Check.Speed;
using KinectControll.Check.Move;
using KinectControll.Check.Distance;

using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.ImageView
{
    /// <summary>
    /// Interaktionslogik für ButtonsControl.xaml
    /// </summary>
    public partial class ImageControl
    {
        public ImageControl()
        {
            InitializeComponent();
        }
    }
}
