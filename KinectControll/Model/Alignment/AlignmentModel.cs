using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace KinectControll.Model.Alignment
{
    /**
     * Stores ALignment Camera States
     * Allows to end all threads (e.g. on application close)
     */
    public class AlignmentModel
    {
        #region Private variables
        private Dictionary<int, CameraVO> _cameraVOs;
        #endregion

        /**
         * This is a Singleton. It's constructor can't be called from aoutside.
         */
        private AlignmentModel()
        {
            _cameraVOs = new Dictionary<int, CameraVO>();
        }

        #region Singleton instantiation
        public static AlignmentModel Instance
        {
            get
            {
                return SingletonCreator.instance;
            }
        }

        /**
         * Nested calss can only be called by ItemManager
         */
        private class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly AlignmentModel instance = new AlignmentModel();
        }
        #endregion

        /**
         * Stores a thread
         */
        public void AddCameraVO(int angle, CameraVO vo)
        {
            _cameraVOs.Add(angle, vo);
        }

        /**
         * Removes a thread from storage and kills it
         */
        public void RemoveVO(int angle)
        {
            // Remove from list
            _cameraVOs.Remove(angle);
        }

        #region Getter and setter methods
        public Dictionary<int, CameraVO> CameraVOs
        {
            get
            {
                return _cameraVOs;
            }
        }
        #endregion
    }
}
