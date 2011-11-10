using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.ImageView;

namespace KinectControll.Demo.Controller.Background
{
    class ShowImageViewControlCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            // Set red background
            SendNotification(DemoApplicationFacade.SET_BACKGROUND, "D:/Programmierung/Kinect/KinectControll/KinectControll/KinectControll/assets/img/red.jpg");

            ImageMediator mediator = (ImageMediator)Facade.RetrieveMediator(ImageMediator.NAME);
            mediator.Show();
        }
    }
}
