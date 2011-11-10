using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to create new threads
using System.Threading;
//
using KinectControll.Manager.Alignment;
// 
using KinectControll.Model.Threading;

namespace KinectControll.Controller.Alignment
{
    public static class AlignmentController
    {
        /**
         * Collect data to base search on
         */
        public static void CollectData()
        {
            CameraPositioning camPos = new CameraPositioning();
            Thread camPosThread = new Thread(new ThreadStart(camPos.ArrangeCamera));

            ThreadModel.Instance.AddThread("camPos", camPosThread);
            camPosThread.Start();
        }

        /**
         * Will seach for best camera angle 
         */
        public static void AlignCamera()
        {
            // Choose best angle
        }

        /**
         * Will enforce an offset on user input to center his position
         */
        public static void AlignUser()
        {
            // Set user offset
        }
    }
}
