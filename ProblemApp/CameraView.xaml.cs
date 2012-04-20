using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices;

namespace ProblemApp
{
    public partial class CameraView : PhoneApplicationPage
    {
        private PhotoCamera _camera;

        public CameraView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeCamera();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //while (_captureInProgress) { };
            _camera.AutoFocusCompleted -= OnCameraAutoFocusCompleted;
            _camera.CaptureImageAvailable -= OnCaptureImageAvailable;
            _camera.Initialized -= OnCameraInitialized;
            _camera.Dispose();
            _camera = null;
            base.OnNavigatedFrom(e);
        }

        private void InitializeCamera()
        {
            _camera = new PhotoCamera();
            _camera.Initialized += OnCameraInitialized;
            viewfinderBrush.SetSource(_camera);
        }

        void OnCameraInitialized(object sender, EventArgs e)
        {
            _camera.Initialized -= OnCameraInitialized;
            _camera.AutoFocusCompleted += OnCameraAutoFocusCompleted;
            _camera.CaptureImageAvailable += OnCaptureImageAvailable;
            _camera.CaptureCompleted += OnCameraCaptureCompleted;

            _camera.FlashMode = FlashMode.Off;
            _camera.Focus();
        }

        private void OnCameraAutoFocusCompleted(object sender, EventArgs e)
        {
            _camera.CaptureImage();
            Dispatcher.BeginInvoke(delegate()
            {
                NavigationService.GoBack();
            });
        }

        private void OnCameraCaptureCompleted(object sender, EventArgs e)
        {

        }

        private void OnCaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {

        }
    }
}