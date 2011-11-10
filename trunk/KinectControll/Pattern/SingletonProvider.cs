using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Pattern
{
    /**
     * This Singleton Provider can be used to instanciate any calss as singleton
     * Code has been adopted from: http://www.codeproject.com/KB/cs/genericsingleton.aspx
     */
    public class SingletonProvider<T> where T : new()
    {
        /**
         * Constructor can't be called from outside
         */
        private SingletonProvider() { }

        /**
         * Returns instance
         */
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }

        /**
         * Nested Singleton creator class
         * Can't be accessed from outside
         */
        private class SingletonCreator
        {
            static SingletonCreator() { }
            internal static readonly T instance = new T();
        }
    }
}
