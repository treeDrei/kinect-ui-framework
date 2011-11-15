using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

using KinectControll.Demo.View.MenuView;
using KinectControll.Demo.View.ImageView;
using KinectControll.Demo.View.HomeView;
using KinectControll.Demo.View.VisualizerView;

using KinectControll.Demo.View.BackgroundView;

namespace KinectControll.Demo.Controller.Navigation
{
    class NavigateHomeCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification notification)
        {
            BackgroundMediator mediator = (BackgroundMediator)Facade.RetrieveMediator(BackgroundMediator.NAME);

            // Change menu view state
            MenuMediator menuMediator = (MenuMediator)Facade.RetrieveMediator(MenuMediator.NAME);
            menuMediator.Show();

            // Change image view state
            ImageMediator imageMediator = (ImageMediator)Facade.RetrieveMediator(ImageMediator.NAME);
            imageMediator.Hide();

            // Change visualizer view state
            VisualizerMediator visualizerMadiator = (VisualizerMediator)Facade.RetrieveMediator(VisualizerMediator.NAME);
            visualizerMadiator.Hide();

            // Change home view state
            HomeMediator homeMediator = (HomeMediator)Facade.RetrieveMediator(HomeMediator.NAME);
            homeMediator.Hide();
        }
    }
}
