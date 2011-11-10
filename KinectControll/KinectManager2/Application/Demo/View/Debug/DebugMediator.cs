using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace WpfSampleApplication.KinectManager.Application.Demo.View.Debug
{
    public class DebugMediator : Mediator, IMediator
    {
        public DebugMediator(DebugView debugView)
            : base(NAME, debugView)
        {
            //debugView.NewUser += new EventHandler(userList_NewUser);
        }
    }
}
