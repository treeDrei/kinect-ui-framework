using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Manager;
using KinectControll.Manager.Input;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeKinectManagerCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            // Creates instance of kinect manager to start kinect
            KinectManager manager = KinectManager.Instance;

            KinectInputManager control = KinectInputManager.Instance;
            // Sets area for kinect controll
            control.Width = 525;
            control.Height = 525;

            control.Multiplyer = 3;
        }
    }
}
