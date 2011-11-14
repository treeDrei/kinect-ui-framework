using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MainView;
using KinectControll.Demo.View.CalibrationView;

using KinectControll.Demo.Pose.HomePose;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeCalibrationCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);

            // Button Control view initialization
            CalibrationView calibrationView = new CalibrationView();
            CalibrationMediator mediator = new CalibrationMediator(calibrationView);
            Facade.RegisterMediator(mediator);
            mainMediator.AddKinectView(calibrationView);

            // Hide on startup
            mediator.Show();
        }
    }
}
