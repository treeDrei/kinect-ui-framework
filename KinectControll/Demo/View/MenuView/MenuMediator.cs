using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.ImageView;

using KinectControll;

namespace KinectControll.Demo.View.MenuView
{
    class MenuMediator : Mediator, IMediator
    {
        public new const string NAME = "MenuMediator";

        MenuView view;

        public MenuMediator(MenuView menuView)
            : base(NAME, menuView)
        {
            view = menuView;
        }

        /**
         * Registers view events
         */
        public override void OnRegister()
        {
            base.OnRegister();
            // Navigate to required view
            view.OnVisualizerNavigation += new EventHandler(VisualizerNavigationHandler);
            view.OnImageNavigation += new EventHandler(ImageNavigationHandler);
            view.OnClose += new EventHandler(CloseHandler);
        }

        /**
         * Will request an application close
         */
        private void CloseHandler(object sender, EventArgs e)
        {
            SendNotification(DemoApplicationFacade.APPLICATION_CLOSE);
        }

        /**
         * Handles view navigation request
         */
        void VisualizerNavigationHandler(object sender, EventArgs e)
        {
            SendNotification(DemoApplicationFacade.NAVIGATE_VISUALIZER);
        }

        void ImageNavigationHandler(object sender, EventArgs e)
        {
            SendNotification(DemoApplicationFacade.NAVIGATE_IMAGE);
        }

        /**
         * Handles hide request
         */
        public void Hide()
        {
            view.SetState(MenuView.HIDE);
        }

        /**
         * Handles show request
         */
        public void Show()
        {
            view.SetState(MenuView.SHOW);
        }

    }
}
