using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to get camera for evaluation
using KinectControll.Manager.Data;

namespace KinectControll.Controller.Alignment
{   
    /**
     * This static class allows camera manipulation
     */
    public static class CameraController
    {
        /**
         * Tries to set an angle to move the camera to
         */
        public static Boolean SetAngle(int angle)
        {
            // Check wether camara is avalable
            if (SDKDataManager.Instance.Runtime.NuiCamera != null)
            {
                try
                {
                    SDKDataManager.Instance.Runtime.NuiCamera.ElevationAngle = angle;
                    return true;
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Angle couldn't be set: " + e.Message);
                    return false;
                }
            }

            return false;
        }
    }
}
