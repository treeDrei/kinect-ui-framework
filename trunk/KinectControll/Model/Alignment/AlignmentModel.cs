using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

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
        // This offste can be applied to all positions
        private Point _offset;
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
        public void SetCameraVO(int angle, CameraVO vo)
        {
            _cameraVOs.Add(angle, vo);
        }

        /**
         * Returns a camera vo
         */
        public CameraVO GetCameraVO(int angle)
        {
            return _cameraVOs[angle];
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

        /**
         * X,Y Offset for head and hands
         */
        public Point Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }

        }

        /**
         * Stores best camera angle
         */
        public int BestAngle { get; set; }
        #endregion
    }
}
