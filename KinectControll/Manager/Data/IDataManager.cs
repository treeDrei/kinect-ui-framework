using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use Data Manager Event Args
using KinectControll.Manager.Data.Event;

namespace KinectControll.Manager.Data
{
    /**
     * Interface defines interfaces for triggerable events
     */
    interface IDataManager
    {
        #region Event
        // The Event will allow external objects to rigister on it
        event EventHandler<DataManagerEventArgs> OnUpdate;
        event EventHandler<DataManagerEventArgs> OnFixedUpdate;
        #endregion

        #region Methods
        // Starts data stream
        void Start();
        // Stops data stream
        void Stop();
        // Sets deley to fixed update
        void SetFixedUpdateDelay(int delay);
        #endregion
    }
}
