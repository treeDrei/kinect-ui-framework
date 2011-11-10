using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.ImageView;

using KinectControll;

namespace KinectControll.Demo.View.HomeView
{
    class HomeMediator : Mediator, IMediator
    {
        public new const string NAME = "HomeMediator";

        HomeView view;

        public HomeMediator(HomeView homeView)
            : base(NAME, homeView)
        {
            view = homeView;
        }

        /**
         * Registers view events
         */
        public override void OnRegister()
        {
            base.OnRegister();
            // Navigate to required view
            view.OnNavigation += new EventHandler(NavigationHandler);
        }

        /**
         * Handles view navigation request
         */
        void NavigationHandler(object sender, EventArgs e)
        {
            // Navigate home
            SendNotification(DemoApplicationFacade.NAVIGATE_HOME);
        }

        #region Mediator interaction
        /**
         * Handles hide request
         */
        public void Hide()
        {
            view.SetState(HomeView.HIDE);
        }

        /**
         * Handles show request
         */
        public void Show()
        {
            view.SetState(HomeView.SHOW);
        }
        #endregion

    }
}
