using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.BackgroundView;

namespace KinectControll.Demo.Controller.Background
{
    class SetBackgroundControlCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            BackgroundMediator mediator = (BackgroundMediator)Facade.RetrieveMediator(BackgroundMediator.NAME);

            String path = (String)notification.Body;
            mediator.SetBackground(path);

        }
    }
}
