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

            this.Cursor = System.Windows.Input.Cursors.None;
            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;

            // ResizeMode="NoResize" WindowStyle="None" WindowState="Maximized" Topmost="True" 
            // Put it back in xaml for REAL full screen
        }

        /**
         * Adds a view to main window
         */
        public void AddView(System.Windows.Controls.UserControl view)
        {
            this.LayoutRoot.Children.Add(view);
        }
    }
}
