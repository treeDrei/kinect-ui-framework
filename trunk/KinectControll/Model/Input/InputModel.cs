using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Required to use Point
using System.Windows;
// Singleton Pattern
using KinectControll.Pattern;

namespace KinectControll.Model.Input
{
    class InputModel
    {

        #region Singleton instantiation
        /**
         * Singleton shouldn't be instantiated from outside
         */
        public InputModel() 
        {
            Multiplyer = 1;
        }
        
        public static InputModel Instance
        {
            get
            {
                return SingletonProvider<InputModel>.Instance;
            }
        }
        #endregion

        /**
         * Current selection 
         */
        public Point Current{ get; set; }

        /**
         * Width getter
         */
        public double Width{ get; set; }
        
        /**
         * Height set & get
         */
        public double Height { get; set; }

        /**
         * Height set & get
         */
        public double Multiplyer { get; set; }

        /**
         * Height set & get
         */
        public Boolean IsLeft { get; set; }


    }
}
