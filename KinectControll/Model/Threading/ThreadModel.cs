﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using KinectControll.Pattern;

namespace KinectControll.Model.Threading
{
    /**
     * Stores all running threads
     * Allows to end all threads (e.g. on application close)
     */
    public class ThreadModel
    {
        #region Private variables
        private Dictionary<String, Thread> _threads;
        #endregion

        public ThreadModel()
        {
            _threads = new Dictionary<String, Thread>();
        }

        #region Singleton instantiation
        public static ThreadModel Instance
        {
            get
            {
                return SingletonProvider<ThreadModel>.Instance;
            }
        }
        #endregion

        /**
         * Stores a thread
         */
        public void AddThread(String id, Thread thread)
        {
            // Try to remove
            RemoveThread(id);
            // Add
            _threads.Add(id, thread);
        }

        /**
         * Removes a thread from storage and kills it
         */
        public void RemoveThread(String id)
        {
            if (_threads.ContainsKey(id))
            {
                Thread toRemove = _threads[id];
                // Thread is beeing run right now
                if (toRemove != null & toRemove.IsAlive)
                {
                    // Stop thread if running
                    toRemove.Abort();
                }

                // Remove from list
                _threads.Remove(id);
            }
        }

        #region Getter and setter methods
        public Dictionary<String, Thread> Threads
        {
            get
            {
                return _threads;
            }
        }
        #endregion
    }
}
