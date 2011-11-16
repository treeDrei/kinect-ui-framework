using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Manager.Data;

using Microsoft.Research.Kinect.Nui;

// Required to use debug view
using KinectControll.Demo.View.DebugView;
// Required to close main window
using KinectControll.Demo.View.MainView;

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
            if (debugMediator != null)
            {
                // Close debug window
                debugMediator.close();
            }

            MainMediator mainMediator = (MainMediator)Facade.RetrieveMediator(MainMediator.NAME);
            if (mainMediator != null)
            {
                mainMediator.close();
            }

            /*
            // Get kinect manager
            KinectManager manager = KinectManager.Instance;
            // Stop manager
            manager.Runtime.Uninitialize();
             * */
            SDKDataManager.Instance.Stop();

            // Ends all running threads
            ThreadController.EndAll();
        }
    }
}
