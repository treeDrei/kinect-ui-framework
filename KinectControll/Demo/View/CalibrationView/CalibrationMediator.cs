using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.ImageView;

using KinectControll;

namespace KinectControll.Demo.View.CalibrationView
{
    class CalibrationMediator : Mediator, IMediator
    {
        public new const string NAME = "CalibrationMediator";

        CalibrationView view;

        public CalibrationMediator(CalibrationView homeView)
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
            // Request new calibration -> this could cause multiple threads -> TO-DO
            SendNotification(DemoApplicationFacade.CALIBRATION);
        }

        #region Mediator interaction
        /**
         * Handles hide request
         */
        public void Hide()
        {
            view.SetState(CalibrationView.HIDE);
        }

        /**
         * Handles show request
         */
        public void Show()
        {
            view.SetState(CalibrationView.SHOW);
        }
        #endregion

    }
}
