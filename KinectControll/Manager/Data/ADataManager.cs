using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Required to use data manager event args
using KinectControll.Manager.Data.Event;

namespace KinectControll.Manager.Data
{
    /**
     * Abstract class will force trigger implementation 
     */
    abstract public class ADataManager
    {
        #region Event

        /**
         * Event dispatch trigger
         */
        abstract protected void TriggerUpdate(DataManagerEventArgs args);
        abstract protected void TriggerFixedUpdate(DataManagerEventArgs args);
        #endregion
    }
}
