using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

//using KinectControll.Demo.View.Component;

namespace KinectControll.Demo.View.HandView
{
    public class HandMediator : Mediator, IMediator
    {
        public new const string NAME = "HandMediator";

        HandView view;

        public HandMediator(HandView handView)
            : base(NAME, handView)
        {
            view = handView;
        }

        /**
         * Shows hand view
         */
        public void Show()
        {
            view.SetState(HandView.SHOW);
        }

        /**
         * Hides hand view
         */
        public void Hide()
        {
            view.SetState(HandView.HIDE);
        }
    }
}
