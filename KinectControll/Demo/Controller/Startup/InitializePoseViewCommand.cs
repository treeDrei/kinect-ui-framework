using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Pure mvc components
using PureMVC.Patterns;
using PureMVC.Interfaces;

// Pose view will be instantiated
using KinectControll.Demo.View.PoseView;
// Required to add view to main view
using KinectControll.Demo.View.MainView;
namespace KinectControll.Demo.Controller.Startup
{
    class InitializePoseViewCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Hand Control view initialization
            PoseView poseView = new PoseView();
            PoseMediator poseMediator = new PoseMediator(poseView);
            poseMediator.Show();
            Facade.RegisterMediator(poseMediator);
            mainMediator.AddKinectView(poseView);
        }
    }
}
