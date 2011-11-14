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

using Microsoft.Research.Kinect.Nui;

using KinectControll.Demo.View.BaseView;
using KinectControll.Manager;

namespace KinectControll.Demo.View.MainView
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Runtime nui;

        public MainWindow()
        {
            InitializeComponent();

            this.Cursor = Cursors.None;
        }

        /**
         * Adds a view to main window
         */
        public void AddView(UserControl view)
        {
            this.LayoutRoot.Children.Add(view);
        }
    }
}
