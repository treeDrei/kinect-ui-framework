using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to create new threads
using System.Threading;
// 
using KinectControll.Model.Threading;
//
using KinectControll.Threading.Alignment;

namespace KinectControll.Controller.Alignment
{
    public static class AlignmentController
    {
        /**
         * Collect data to base search on
         */
        public static void Arrange()
        {
            // Creates threadable class
            AlignmentThread alignmentThread = new AlignmentThread();
            // Tells thread wich function to run
            Thread thread = new Thread(new ThreadStart(alignmentThread.Run));

            // if this function is called again, befor the thread has finished, it will be destroyed by model, because the identifyer is the same
            ThreadModel.Instance.AddThread("alignmentThread", thread);
            // Starts thread execution
            thread.Start();
        }

        /**
         * Will seach for best camera angle 
         *
        public static void AlignCamera()
        {
            // Choose best angle
        }

        /**
         * Will enforce an offset on user input to center his position
         *
        public static void AlignUser()
        {
            // Set user offset
        }
        */
    }
}
