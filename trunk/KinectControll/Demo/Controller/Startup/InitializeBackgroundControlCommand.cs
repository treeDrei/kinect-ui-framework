using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.BackgroundView;
using KinectControll.Demo.View.MainView;
using KinectControll.Demo.View.DebugView;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeBackgroundControlCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Background Control view initialization
            BackgroundControl view = new BackgroundControl();
            BackgroundMediator mediator = new BackgroundMediator(view);

            mediator.SetBackground("/Images/grey.jpg");

            // Reguister mediator to allow later retreival
            Facade.RegisterMediator(mediator);
            // Add view to main view via it's mediator
            mainMediator.AddView(view);
        }
    }
}
