using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace KinectManager.Application.Demo
{
    /**
     * This is the application center where all conections are beeing wired
     */
    class DemoApplicationFacade : Facade
    {
        #region Notification name constants

        public const string STARTUP                                     = "startup";

        public const string NEW_USER                            = "newUser";
        public const string DELETE_USER                         = "deleteUser";
        public const string CANCEL_SELECTED                     = "cancelSelected";

        public const string USER_SELECTED                       = "userSelected";
        public const string USER_ADDED                          = "userAdded";
        public const string USER_UPDATED                        = "userUpdated";
        public const string USER_DELETED                        = "userDeleted";
        public const string ADD_ROLE                            = "addRole";
        public const string ADD_ROLE_RESULT                     = "addRoleResult";

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
        public void Startup(object app)
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
            //RegisterCommand(STARTUP, typeof(StartupCommand));
            //RegisterCommand(DELETE_USER, typeof(DeleteUserCommand));
            //RegisterCommand(ADD_ROLE_RESULT, typeof(AddRoleResultCommand));
        }

        #endregion
    }
}
