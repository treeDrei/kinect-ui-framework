using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectControll.Manager
{
    class Singleton
    {
        /**
         * Thread safe singleton intsantiation
         */
        public static Singleton Instance
        {
            get
            {
                try
                {
                    return Nested.instance;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Du musst den Strom stecker der Kinect einstöpseln!... Meine Fresse...");
                    Console.WriteLine(e.Message);
                }
                return null;
            }
        }

        /**
         * Nested calss can instantiate singleton
         */
        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Singleton instance = new Singleton();
        }
    }
}
