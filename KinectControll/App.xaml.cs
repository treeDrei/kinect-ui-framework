using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using KinectControll.Demo;

namespace KinectControll
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DemoApplicationFacade facade = (DemoApplicationFacade) DemoApplicationFacade.Instance;
            facade.Startup();
        }
    }
}
