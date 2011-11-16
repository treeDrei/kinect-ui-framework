using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using System.Windows.Controls;

using KinectControll.Demo.View.BaseView;

namespace KinectControll.Demo.View.MainView
{
    class MainMediator : Mediator, IMediator
    {
        public new const string NAME = "MainMediator";

        MainWindow view;

        public MainMediator(MainWindow mainView) : base(NAME, mainView)
        {
            view = mainView;
            view.Show();
        }

        /**
         * Registers on view events
         */
        public override void OnRegister()
        {
            base.OnRegister();
            view.Closed += new EventHandler(viewClosedHander);
        }

        /**
         * Request application close on main window close
         */
        void viewClosedHander(object sender, EventArgs e)
        {
            SendNotification(DemoApplicationFacade.APPLICATION_CLOSE, null);
        }

        /**
         * Adds a view to main window
         */
        public void AddView(UserControl component)
        {
            view.AddView(component);
            //(component as IKinectView).SetParent(view as IKinectView);
        }

        /**
         * Adds a view of BaseKinectView type
         */
        public void AddKinectView(BaseKinectView kinectView)
        {
            // Adds user control to main window
            view.AddView(kinectView.GetUSerControl());
        }

        /**
         * Will close main window
         */
        public void close()
        {
            view.Close();
        }
    }
}
