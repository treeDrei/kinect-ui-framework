using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Manager.Data;

using Microsoft.Research.Kinect.Nui;

using KinectControll.Demo.View.DebugView;

// Required for thread manipulation
using KinectControll.Controller.Threading;

namespace KinectControll.Demo.Controller
{
    class EndApplicationCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            // Retrieve mediator instance from fassade
            DebugMediator debugMediator = (DebugMediator)Facade.RetrieveMediator("DebugMediator");
            // Close debug window
            debugMediator.close();

            /*
            // Get kinect manager
            KinectManager manager = KinectManager.Instance;
            // Stop manager
            manager.Runtime.Uninitialize();
             * */
            KinectDataManager.Instance.Stop();

            // Ends all running threads
            ThreadController.EndAll();
        }
    }
}
