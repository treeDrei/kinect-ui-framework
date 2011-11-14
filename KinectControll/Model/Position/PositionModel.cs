using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using KinectControll.Pattern;

namespace KinectControll.Model.Position
{
    /**
     * Stores all running threads
     * Allows to end all threads (e.g. on application close)
     */
    public class PositionModel
    {
        #region Private variables
        private PositionVO _current;
        private PositionVO _normed;
        #endregion

        public PositionModel()
        {
        }

        #region Singleton instantiation
        public static PositionModel Instance
        {
            get
            {
                return SingletonProvider<PositionModel>.Instance;
            }
        }
        #endregion

       
        #region Getter and setter methods
        public PositionVO Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }

        }

        /**
         * A normed data set
         */
        public PositionVO Normed
        {
            get
            {
                return _normed;
            }
            set
            {
                _normed = value;
            }

        }
        #endregion
    }
}
