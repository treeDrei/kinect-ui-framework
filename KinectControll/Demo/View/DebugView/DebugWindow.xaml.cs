using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Microsoft.Research.Kinect.Nui;

using KinectControll.Manager.Data;
using KinectControll.Manager.Input;
using KinectControll.Manager.Input.Event;

namespace KinectControll.Demo.View.DebugView
{
    /// <summary>
    /// Interaktionslogik für DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        // Get natural user interface runtime from manager
        Runtime nui;

        public DebugWindow()
        {
            InitializeComponent();
        }

        /**
        * Wait until application is loaded
        */
        private void windowLoadedHandler(object sender, RoutedEventArgs e)
        {
            nui = KinectDataManager.Instance.Runtime;
            KinectInputManager control = KinectInputManager.Instance;
            control.OnChanged += new EventHandler<KinectInputManagerEventArgs>(controlChangedHandler);

            debugOutput.Content = "Loaded hat geklappt...";

            // Adds an eventlistener to VideoFrameReady
            nui.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(videoFrameReadyHandler);
            // Adds eventlistener for depth frame ready (double tap will auto generate event handler)
            nui.DepthFrameReady += new EventHandler<ImageFrameReadyEventArgs>(depthFrameReadyHandler);
            // Adds eventListener for skeleton frame ready
            nui.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(skeletonFrameReadyHandler);

            //setSmoothing();

            debugOutput.Content = "test";
        }

        /**
         * Updates hand position on view
         */
        void controlChangedHandler(object sender, KinectInputManagerEventArgs e)
        {
            Canvas.SetLeft(controllElipse, e.xPos);
            Canvas.SetTop(controllElipse, e.yPos);
            debugOutput.Content = "Event: " + e.xPos + "/" + e.yPos;
        }

        /**
         * Handles skeleton data
         */
        private void skeletonFrameReadyHandler(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Up to 8 skeletons will be found by kniect. Only two can be tracked
            SkeletonFrame allSkeletons = e.SkeletonFrame;

            // Get first tracked skeleton
            SkeletonData firstSkeleton = (from s in allSkeletons.Skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();

            if (firstSkeleton != null)
            {
                // Set joints to be displayed
                setEllipsePosition(headEllipse, firstSkeleton.Joints[JointID.Head]);
                setEllipsePosition(leftEllipse, firstSkeleton.Joints[JointID.HandLeft]);
                setEllipsePosition(rightEllipse, firstSkeleton.Joints[JointID.HandRight]);
            }

            //SkeletonData secondSkeleton = (from s in allSkeletons.Skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).Last();
        }

        /**
         * Sets a ellipse to joint position
         */
        private void setEllipsePosition(FrameworkElement ellipse, Joint joint)
        {
            //var scaledJoint = joint.scaleTo(640, 480, .5f, .5f);

            //debugOutput.Content = joint.Position.X + " / " + joint.Position.Y;

            //Canvas.SetLeft(ellipse, 320 - ((-1 * joint.Position.X) * 320));
            //Canvas.SetTop(ellipse, 240 - (joint.Position.Y * 240));


            float size = 50 / (joint.Position.Z + 1) * 3;
            ellipse.Width = size;
            ellipse.Height = size;

            Canvas.SetLeft(ellipse, (-1 * joint.Position.X * 320) - size/2);
            Canvas.SetTop(ellipse, (-1* joint.Position.Y * 240)-size/2);
            
            
        }

        /**
         * Handles new depth frame
         */
        void depthFrameReadyHandler(object sender, ImageFrameReadyEventArgs e)
        {
            // Get image from event payload (current camera image)
            PlanarImage image = e.ImageFrame.Image;

            // Converts depth information into color for each pixel
            Byte[] coloredBytes = generateColoredBytes(e.ImageFrame);

            // Dots per inch
            int dpi = 96;
            // Image with plus padding on the right
            int stride = image.Width * PixelFormats.Bgr32.BitsPerPixel / 8;
            // Set image src on xaml item videoImage
            depthImage.Source = BitmapSource.Create(image.Width, image.Height, dpi, dpi, PixelFormats.Bgr32, null, coloredBytes, stride);
        }

        /**
         * Generates colord image byte array from image frame
         */
        private byte[] generateColoredBytes(ImageFrame imageFrame)
        {
            int height = imageFrame.Image.Height;
            int width = imageFrame.Image.Width;

            // Depth data for each pixel
            Byte[] depthData = imageFrame.Image.Bits;

            // Color frame contains color information for each pixel in frame
            // Height * width * 4 (red, green, blue, empty byte)
            Byte[] colorData = new byte[height * width * 4];

            // Brg is blue, green, red, empty byte
            // Bgra is blue, green, red, alpha transperecy (has to be set since default is 0 = fully transparent)

            var depthIndex = 0;
            for (var y = 0; y < height; y++)
            {
                // Color change for diffent hights at a certain distance
                var heightOffset = y * width;
                for (var x = 0; x < width; x++)
                {
                    var index = ((width - x - 1) + heightOffset) * 4;

                    // Filters distance from byte stream
                    var distance = getDistanceWithPlayerIndex(depthData[depthIndex], depthData[depthIndex + 1]);
                    // Sets color for distance
                    setColorByDistance(distance, colorData, index);

                    // Gets player from frame
                    int player = getPlayerIndex(depthData[depthIndex]);
                    // Sets color for player
                    setColorByPlayer(player, colorData, index);

                    depthIndex += 2;
                }
            }

            return colorData;
        }

        // Hardcoded location to blue, green, red index positions
        const int blueIndex = 0;
        const int greenIndex = 1;
        const int redIndex = 2;

        /**
         * Color will be set by distance
         */
        private void setColorByDistance(int distance, Byte[] colorData, int index)
        {
            // Close
            if (distance <= 900)
            {
                colorData[index + blueIndex] = 255;
                colorData[index + greenIndex] = 0;
                colorData[index + redIndex] = 0;
            }
            // Middel
            else if (distance > 900 && distance <= 2000)
            {
                colorData[index + blueIndex] = 0;
                colorData[index + greenIndex] = 255;
                colorData[index + redIndex] = 0;
            }
            // Far away
            else if (distance > 2000)
            {
                colorData[index + blueIndex] = 0;
                colorData[index + greenIndex] = 255;
                colorData[index + redIndex] = 255;
            }
        }

        /**
         * Color will be set if player is found
         */
        private void setColorByPlayer(int player, Byte[] colorData, int index)
        {
            // 0 = no player, 1 = first, 2 = second
            if (player == 1)
            {
                colorData[index + blueIndex] = 155;
                colorData[index + greenIndex] = 155;
                colorData[index + redIndex] = 155;
            }
            else if (player == 2)
            {
                colorData[index + blueIndex] = 055;
                colorData[index + greenIndex] = 055;
                colorData[index + redIndex] = 055;
            }

        }


        /**
         * gets distance from frame data
         */
        private int getDistanceWithPlayerIndex(byte firstFrame, byte secondFrame)
        {
            // Barrel shift by 3 bytes to get values after player index in frame data
            int distance = (int)(firstFrame >> 3 | secondFrame << 5);
            return distance;
        }

        private int getPlayerIndex(byte firstFrame)
        {
            // 0 = no player, 1 = first player, 2 = second player
            return (int)firstFrame & 7;
        }

        /**
         * Handles new frame from video camera
         */
        void videoFrameReadyHandler(object sender, ImageFrameReadyEventArgs e)
        {
            // Get image from event payload (current camera image)
            PlanarImage image = e.ImageFrame.Image;
            // Dots per inch
            int dpi = 96;
            // Image with plus padding on the right
            int stride = image.Width * image.BytesPerPixel;
            // Set image src on xaml item videoImage
            videoImage.Source = BitmapSource.Create(image.Width, image.Height, dpi, dpi, PixelFormats.Bgr32, null, image.Bits, stride);
        }
    }
}
