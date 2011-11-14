using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView.Event;
using KinectControll.Demo.View.ImageView;

using KinectControll.Demo.View.BaseView;

using KinectControll;

namespace KinectControll.Demo.View.PoseView
{
    class PoseMediator : Mediator, IMediator
    {
        public new const string NAME = "PoseMediator";

        PoseView view;

        public PoseMediator(PoseView homeView)
            : base(NAME, homeView)
        {
            view = homeView;
        }

        #region Mediator interaction
        /**
         * Handles hide request
         */
        public void Hide()
        {
            view.SetState(BaseKinectView.HIDE);
        }

        /**
         * Handles show request
         */
        public void Show()
        {
            view.SetState(BaseKinectView.SHOW);
        }
        #endregion

    }
}
