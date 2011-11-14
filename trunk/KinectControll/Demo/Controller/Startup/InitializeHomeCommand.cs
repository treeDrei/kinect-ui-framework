using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MainView;
using KinectControll.Demo.View.HomeView;

using KinectControll.Demo.Pose.HomePose;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeHomeCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Button Control view initialization
            HomeView homeView = new HomeView();
            HomeMediator mediator = new HomeMediator(homeView);
            Facade.RegisterMediator(mediator);
            mainMediator.AddKinectView(homeView);

            // Hide on startup
            mediator.Hide();

            HomePoseMediator homePoseMediator = new HomePoseMediator();
        }
    }
}
