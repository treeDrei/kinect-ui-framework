using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required for thread manipulation
using System.Threading;
//
using KinectControll.Model.Threading;

namespace KinectControll.Controller.Threading
{
    public static class ThreadController
    {
        /**
         * Ends all running threads
         * (Required on programm ending)
         */
        public static void EndAll()
        {
            ThreadModel model = ThreadModel.Instance;
            try
            {
                foreach (KeyValuePair<String, Thread> keyValuePair in model.Threads)
                {
                    // Remove every thread from model
                    model.RemoveThread(keyValuePair.Key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An erroro occured: " + e.Message);
            }
        }
    }
}
