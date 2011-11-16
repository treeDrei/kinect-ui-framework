using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MainView;
using KinectControll.Demo.View.DebugView;

namespace KinectControll.Demo.Controller.Startup
{
    public class StartupCommand : SimpleCommand, ICommand
    {
        /// <summary>
        /// Register the Proxies and Mediators.
        ///
        /// Get the View Components for the Mediators from the app,
        /// which passed a reference to itself on the notification.
        /// </summary>
        /// <param name="note"></param>
        public override void Execute(INotification note)
        {
            // Prepare Kinect Manager for useage
            SendNotification(DemoApplicationFacade.INITIALIZE_KINECT_MANAGER, null);

            // Register Debug view and mediator
            Facade.RegisterMediator(new DebugMediator(new DebugWindow()));

            // Get main window from payload
            MainWindow mainWindow = (MainWindow) note.Body;
            // Create main window if it hasn't been attached
            if (mainWindow == null)
            {
                mainWindow = new MainWindow();
            }

            // Create main mediator and tell it to mediate main window
            MainMediator mainMediator = new MainMediator(mainWindow);
            // Register mediator to facade
            Facade.RegisterMediator(mainMediator);         

            SendNotification(DemoApplicationFacade.INITIALIZE_BACKGROUND_CONTROL);
            SendNotification(DemoApplicationFacade.INITIALIZE_BUTTONS_CONTROL);
            SendNotification(DemoApplicationFacade.INITIALIZE_IMAGE_VIEW);
            SendNotification(DemoApplicationFacade.INITIALIZE_VISUALIZER_VIEW);

            // Single overlay elements must be second heighest
            SendNotification(DemoApplicationFacade.INITIALIZE_HOME_VIEW);
            SendNotification(DemoApplicationFacade.INITIALIZE_POSE_VIEW);
            SendNotification(DemoApplicationFacade.INITIALIZE_CALIBRATION_VIEW);

            // Has to stay on top
            SendNotification(DemoApplicationFacade.INITIALIZE_HAND_VIEW);
        }
    }
}
