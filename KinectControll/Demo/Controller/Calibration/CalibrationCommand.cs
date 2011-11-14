using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Controller.Alignment;

using KinectControll.Demo.View.BackgroundView;

namespace KinectControll.Demo.Controller.Calibration
{
    class CalibrationCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            AlignmentController.CollectData();
        }
    }
}
