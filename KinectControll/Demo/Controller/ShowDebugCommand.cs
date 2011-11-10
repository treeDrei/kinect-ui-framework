using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.DebugView;

namespace KinectControll.Demo.Controller
{
    public class ShowDebugCommand : SimpleCommand, ICommand 
    {
        public override void Execute(INotification notification)
        {
            DebugMediator mediator = (DebugMediator)Facade.RetrieveMediator("DebugMediator");
            mediator.Show();
        }
    }
}
