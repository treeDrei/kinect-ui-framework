using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MainView;
using KinectControll.Demo.View.MenuView;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeMenuViewCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Button Control view initialization
            MenuView menuView = new MenuView();
            MenuMediator mediator = new MenuMediator(menuView);
            Facade.RegisterMediator(mediator);
            mainMediator.AddKinectView(menuView);

            //mediator.Hide();
        }
    }
}
