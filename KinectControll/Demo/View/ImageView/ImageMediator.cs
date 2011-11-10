using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView;
using KinectControll.Demo.View.MenuView.Event;

using KinectControll;

namespace KinectControll.Demo.View.ImageView
{
    class ImageMediator : Mediator, IMediator
    {
        public new const string NAME = "ImageMediator";

        ImageView view;

        public ImageMediator(ImageView imageView)
            : base(NAME, imageView)
        {
            view = imageView;
        }

        /**
         * Registers view events
         */
        public override void OnRegister()
        {
            base.OnRegister();
        }

        /**
         * Will show view instance
         */
        public void Show()
        {
            view.SetState(ImageView.SHOW);
        }

        /**
         * Will show view instance
         */
        public void Hide()
        {
            view.SetState(ImageView.HIDE);
        }
    }
}
