using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Core;

using KinectControll.Demo.Controller;
using KinectControll.Demo.Controller.Navigation;
using KinectControll.Demo.Controller.Startup;
using KinectControll.Demo.Controller.Background;
using KinectControll.Demo.Controller.Calibration;

namespace KinectControll.Demo
{
    /**
     * This is the application center where all conections are beeing wired
     */
    class DemoApplicationFacade : Facade
    {
        #region Notification name constants

        public const String STARTUP                             = "startup";
        public const String INITIALIZE_KINECT_MANAGER           = "initializeKinectManager";
        public const String INITIALIZE_HAND_VIEW                = "initializeHandView";
        public const String INITIALIZE_BUTTONS_CONTROL          = "initializeButtonsControl";
        public const String INITIALIZE_IMAGE_VIEW               = "initializeImageView";
        public const String INITIALIZE_HOME_VIEW                = "initializeHomeView";
        public const String INITIALIZE_BACKGROUND_CONTROL       = "initializeBackgourndControl";
        public const String INITIALIZE_POSE_VIEW                = "initializePoseView";
        public const String INITIALIZE_CALIBRATION_VIEW         = "initializeCalibrationView";
        public const String INITIALIZE_VISUALIZER_VIEW          = "initializeVisualizerView";

        public const String APPLICATION_CLOSE                   = "applicationClose";

        public const String CALIBRATION                         = "calibration";

        public const String NAVIGATE_HOME                       = "navigateHome";
        public const String NAVIGATE_IMAGE                      = "navigateImage";
        public const String NAVIGATE_VISUALIZER                 = "navigateVisualizer";

        public const String SET_BACKGROUND                      = "setBackground";

        #endregion
        
        #region Accessors
        
        /// <summary>
        /// Facade Singleton Factory method.  This method is thread safe.
        /// </summary>
        public new static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new DemoApplicationFacade();
                    }
                }

                return m_instance;
            }
        }

        #endregion
        
        #region Public methods

        /// <summary>
        /// Start the application
        /// </summary>
        /// <param name="app"></param>
        public void Startup(object app = null)
        {
            SendNotification(STARTUP, app);
        }

        #endregion

        #region Protected & Internal Methods

        protected DemoApplicationFacade()
        {
            // Protected constructor.
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit
        ///</summary>
        static DemoApplicationFacade()
        {
        }

        /// <summary>
        /// Register Commands with the Controller
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            
            // Starts application
            RegisterCommand(STARTUP, typeof(StartupCommand));
            
            // Initializes kinect manager to be used in application
            RegisterCommand(INITIALIZE_KINECT_MANAGER, typeof(InitializeKinectManagerCommand));

            // Init view components
            RegisterCommand(INITIALIZE_HAND_VIEW, typeof(InitializeHandViewCommand));
            RegisterCommand(INITIALIZE_BUTTONS_CONTROL, typeof(InitializeMenuViewCommand));
            RegisterCommand(INITIALIZE_IMAGE_VIEW, typeof(InitializeImageControlCommand));
            RegisterCommand(INITIALIZE_BACKGROUND_CONTROL, typeof(InitializeBackgroundControlCommand));
            RegisterCommand(INITIALIZE_HOME_VIEW, typeof(InitializeHomeCommand));
            RegisterCommand(INITIALIZE_POSE_VIEW, typeof(InitializePoseViewCommand));
            RegisterCommand(INITIALIZE_CALIBRATION_VIEW, typeof(InitializeCalibrationCommand));
            RegisterCommand(INITIALIZE_VISUALIZER_VIEW, typeof(InitializeVisualizerControlCommand));

            // Background manipulation
            RegisterCommand(SET_BACKGROUND, typeof(SetBackgroundControlCommand));

            // Closes all windows and stops kinect manager
            RegisterCommand(APPLICATION_CLOSE, typeof(EndApplicationCommand));

            // Requests calibration
            RegisterCommand(CALIBRATION, typeof(CalibrationCommand));

            // Navigation
            RegisterCommand(NAVIGATE_HOME, typeof(NavigateHomeCommand));
            RegisterCommand(NAVIGATE_IMAGE, typeof(NavigateImageCommand));
            RegisterCommand(NAVIGATE_VISUALIZER, typeof(NavigateVisualizerCommand));
        }

        #endregion
    }
}
