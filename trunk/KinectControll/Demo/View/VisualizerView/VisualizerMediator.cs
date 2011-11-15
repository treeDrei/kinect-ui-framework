using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.ImageView;

using KinectControll;

namespace KinectControll.Demo.View.VisualizerView
{
    class VisualizerMediator : Mediator, IMediator
    {
        public new const string NAME = "VisualizerMediator";

        VisualizerView view;

        public VisualizerMediator(VisualizerView visualizerView)
            : base(NAME, visualizerView)
        {
            view = visualizerView;
        }

        /**
         * Registers view events
         */
        public override void OnRegister()
        {
            base.OnRegister();
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
            view.SetState(VisualizerView.HIDE);
        }

        /**
         * Handles show request
         */
        public void Show()
        {
            view.SetState(VisualizerView.SHOW);
        }
        #endregion

    }
}
