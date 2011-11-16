using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace KinectControll.Demo.View.DebugView
{
    public class DebugMediator : Mediator, IMediator
    {
        public new const string NAME = "DebugMediator";

        DebugWindow view;

        public DebugMediator(DebugWindow debugView) : base(NAME, debugView)
        {
            view = debugView;
            view.Show();
        }

        /**
         * Shows debug window
         */
        public void Show()
        {
            view.Show();  
        }

        /**
         * Hides debug window
         */
        public void Hide()
        {
            view.Hide();
        }

        /**
         * Closes this window
         */
        public void close()
        {
            if(view != null)
            {
                view.Close();
                view = null;
            }
        }

        /**
         * Will open a new window if it has been closed
         */
        public void open()
        {
            if (view == null)
            {
                view = new DebugWindow();
                view.Show();
            }
        }

    }
}
