using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace KinectControll.Demo.View.BackgroundView
{
    class BackgroundMediator : Mediator, IMediator
    {
        public new const string NAME = "BackgroundMediator";

        BackgroundControl view;

        public BackgroundMediator(BackgroundControl backgroundView) : base(NAME, backgroundView)
        {
            view = backgroundView;
        }

        /**
         * Changes background image on view
         */
        public void SetBackground(String path)
        {
            view.SetImage(path);
        }
    }
}
