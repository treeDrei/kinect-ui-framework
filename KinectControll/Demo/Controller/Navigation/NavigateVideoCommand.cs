using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView;
using KinectControll.Demo.View.ImageView;
using KinectControll.Demo.View.HomeView;

using KinectControll.Demo.View.BackgroundView;

namespace KinectControll.Demo.Controller.Navigation
{
    class NavigateVideoCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            // Change background
            SendNotification(DemoApplicationFacade.SET_BACKGROUND, "D:/Programmierung/Kinect/KinectControll/KinectControll/KinectControll/assets/img/grey.jpg");

            // Change menu view state
            MenuMediator menuMediator = (MenuMediator)Facade.RetrieveMediator(MenuMediator.NAME);
            menuMediator.Hide();

            // Change image view state
            ImageMediator imageMediator = (ImageMediator)Facade.RetrieveMediator(ImageMediator.NAME);
            imageMediator.Show();

            // Change home view state
            HomeMediator homeMediator = (HomeMediator)Facade.RetrieveMediator(HomeMediator.NAME);
            homeMediator.Show();
        }
    }
}