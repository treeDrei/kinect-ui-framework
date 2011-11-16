using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

// Required for kinect control start
using KinectControll.Manager;
// Required for input model
using KinectControll.Model.Input;
// 
using KinectControll.Controller.Alignment;

using System.Windows.Forms;
using System.Drawing;

namespace KinectControll.Demo.Controller.Startup
{
    class InitializeKinectManagerCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            // Creates instance of kinect manager to start kinect
            KinectManager.Instance.Start();

            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            
            InputModel inputModel = InputModel.Instance;
            // Sets area for kinect controll
            inputModel.Width = bounds.Width;//525;
            inputModel.Height = bounds.Height; //525;

            inputModel.Multiplyer = 3;

            // Call of static function to request camera alignment
            AlignmentController.Arrange();
        }
    }
}
