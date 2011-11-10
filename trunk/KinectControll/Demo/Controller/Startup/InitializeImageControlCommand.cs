using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.ImageView;
using KinectControll.Demo.View.MainView;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeImageControlCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Background Control view initialization
            ImageView view = new ImageView();
            ImageMediator mediator = new ImageMediator(view);
            mediator.Hide();

            // Reguister mediator to allow later retreival
            Facade.RegisterMediator(mediator);
            // Add view to main view via it's mediator
            mainMediator.AddKinectView(view);
        }
    }
}
