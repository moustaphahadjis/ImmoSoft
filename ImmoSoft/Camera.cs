using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Management;
using GitHub.secile.Video;

namespace ImmoSoft
{
    public partial class Camera : Form
    {
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Thread camera;
        bool isCameraRunning = false;
        string path = "";
        public bool taken;
        public Camera()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CaptureCamera();
            path= System.IO.Path.GetTempPath()+ DateTime.Now.ToString("HH-mm-ss")+".jpeg";
        }

        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
            isCameraRunning = true;
            taken=false;
        }
        void usb()
        {
            string[] devices = UsbCamera.FindDevices();
            if (devices.Length == 0) return; // no camera.

            var cameraIndex = 0;
            var formats = UsbCamera.GetVideoFormat(cameraIndex);

            // select the format you want.
            foreach (var item in formats) Console.WriteLine(item);
            // for example, video format is like as follows.
            // 0:[Video], [MJPG], {Width=1280, Height=720}, 333333, [VideoInfo], ...
            // 1:[Video], [MJPG], {Width=320, Height=180}, 333333, [VideoInfo], ...
            // 2:[Video], [MJPG], {Width=320, Height=240}, 333333, [VideoInfo], ...
            // ...
            var format = formats[0];
            var camera = new UsbCamera(cameraIndex, format);
            // this closing event handler make sure that the instance is not subject to garbage collection.
            this.FormClosing += (s, ev) => camera.Release(); // release when close.

            // show preview on control.
            camera.SetPreviewControl(picture.Handle, picture.ClientSize);
            picture.Resize += (s, ev) => camera.SetPreviewSize(picture.ClientSize); // support resize.

            // start.
            camera.Start();

            // get image.
            // Immediately after starting the USB camera,
            // GetBitmap() fails because image buffer is not prepared yet.
            var bmp = camera.GetBitmap();
        }
        private void CaptureCameraCallback()
        {

            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {
                    try
                    {
                        capture.Read(frame);
                        image = BitmapConverter.ToBitmap(frame);
                        if (picture.Image != null)
                        {
                            picture.Image.Dispose();
                        }
                        picture.Image = image;
                    }
                    catch(Exception) { }
                }
            }
        }
       
        private void Camera_Load(object sender, EventArgs e)
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')"))
            {
                foreach (var device in searcher.Get())
                {
                    comboBox1.Items.Add(device["Caption"].ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isCameraRunning)
            {
                Bitmap snapshot = new Bitmap(picture.Image);


                snapshot.Save(string.Format(path, Guid.NewGuid()), ImageFormat.Jpeg);
                capture.Release();
                camera.Abort();
                isCameraRunning = false;
                taken = true;
                button4.Text="Enregistrer photo";
               // this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void Camera_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosed(e);
            try
            {
                capture.Release();
                camera.Abort();
            }
            catch (Exception ex)
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isCameraRunning = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            taken=false;
            this.Close();
        }
    }
}
