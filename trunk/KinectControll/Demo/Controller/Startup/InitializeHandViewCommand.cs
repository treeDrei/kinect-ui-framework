using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Pure mvc components
using PureMVC.Patterns;
using PureMVC.Interfaces;

// Hand view will be instantiated
using KinectControll.Demo.View.HandView;
// Required to add view to main view
using KinectControll.Demo.View.MainView;
namespace KinectControll.Demo.Controller.Startup
{
    class InitializeHandViewCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Hand Control view initialization
            HandView handView = new HandView();
            Facade.RegisterMediator(new HandMediator(handView));
            mainMediator.AddKinectView(handView);
        }
    }
}
