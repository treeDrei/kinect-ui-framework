using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using Microsoft.Research.Kinect.Audio;

namespace WpfSampleApplication.KinectManager
{
    class KinectManager
    {
        // Debug info will be dispatched if debug is enabled
        public event EventHandler<KinectManagerEventArgs> Changed;    // the Event
        // debug
        private Boolean debug;


        // Natual user interface (Kinect)
        private Runtime nui;
        // List of handled objects
        private List<KinectItem> items;


        

        public KinectManager(Boolean enableDebug = true)
        {
            debug = enableDebug;
            items = new List<KinectItem>();
            nui = new Runtime();
        }

        /**
         * Event dispatch trigger
         */
        protected virtual void OnChanged(string info)    // the Trigger
        {
            EventHandler<KinectManagerEventArgs> handler = Changed;   // make a copy to be more thread-safe
            if (handler != null)
            {
                var args = new KinectManagerEventArgs() { Info = info };  // vary
                handler(this, args);
            }
        }

        /**
         * Initializes reuired listeners
         */
        public void initialize()
        {

            this.OnChanged("lol");
            // Apply multiple options using the "|" pipe as seperator
            nui.Initialize(RuntimeOptions.UseColor | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseDepthAndPlayerIndex);
            // Adds an eventlistener to VideoFrameReady
            nui.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(videoFrameReadyHandler);
            // Adds eventlistener for depth frame ready (double tap will auto generate event handler)
            nui.DepthFrameReady += new EventHandler<ImageFrameReadyEventArgs>(depthFrameReadyHandler);
            // Adds eventListener for skeleton frame ready
            nui.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(skeletonFrameReadyHandler);

            // Number of images required
            int poolSize = 2;
            // Opens video stream from hardware to catch regular images
            nui.VideoStream.Open(ImageStreamType.Video, poolSize, ImageResolution.Resolution640x480, ImageType.Color);

            // Opens depth video stream from kinect hardware
            nui.DepthStream.Open(ImageStreamType.Depth, poolSize, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);
        }

        /**
         * Adds an item to allow interaction with
         */
        public Boolean addItem(KinectItem item)
        {
            // Existing items won't be added twice
            if (!items.Contains(item))
            {
                items.Add(item);
            }

            // Shows wether a new item has been added
            return !items.Contains(item);
        }

    }
}
