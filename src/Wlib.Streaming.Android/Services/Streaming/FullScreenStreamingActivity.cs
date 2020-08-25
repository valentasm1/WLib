using System;
using Android.App;
using Android.Content.Res;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Com.Github.Faucamp.Simplertmp;
using Java.IO;
using Java.Lang;
using Java.Net;
using Net.Ossrs.Yasea;

namespace Wlib.Streaming.Android.Services.Streaming
{
    [Activity]
    public class FullScreenStreamingActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, RtmpHandler.IRtmpListener, SrsRecordHandler.ISrsRecordListener, SrsEncodeHandler.ISrsEncodeListener
    {
        const string rtmpUrl = "rtmp://62.77.152.170:1935/live/test";

        // TODO: Set up local storage/recording
        string recPath = "storage/recording";

        SrsPublisher mPublisher;

        Button btnPublish;

        Button btnSwitchCamera;
        //Button btnRecord;
        //Button btnSwitchEncoder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StreamingLayout);

            btnPublish = (Button) FindViewById(Resource.Id.publish);
            btnSwitchCamera = (Button) FindViewById(Resource.Id.swCam);
            //btnRecord = (Button)FindViewById(Resource.Id.record);
            //btnSwitchEncoder = (Button)FindViewById(Resource.Id.swEnc);
        }

        protected void OnPublishClick(object sender, EventArgs e)
        {
            if (btnPublish.Text == "Publish")
            {
                mPublisher.StartPublish(rtmpUrl);
                mPublisher.StartCamera();

                btnPublish.Text = "Stop";
            }
            else if (btnPublish.Text == "Stop")
            {
                mPublisher.StopPublish();
                //mPublisher.StopRecord();

                btnPublish.Text = "Publish";
                //btnRecord.Text = "Record";
            }
        }

        protected void OnCameraSwitch(object sender, EventArgs e)
        {
            var manager = (CameraManager) GetSystemService(CameraService);
            //mPublisher.SwitchCameraFace((mPublisher.CamraId + 1) % manager.GetCameraIdList().Length);
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        protected void OnRecordClick(object sender, EventArgs e)
        {
            //if (btnRecord.Text == "Record")
            //{
            //    if (mPublisher.StartRecord(recPath))
            //    {
            //        btnRecord.Text = "Pause";
            //    }
            //}
            //else if (btnRecord.Text == "Pause")
            //{
            //    mPublisher.PauseRecord();
            //    btnRecord.Text = "Resume";
            //}
            //else if (btnRecord.Text == "Resume")
            //{
            //    mPublisher.ResumeRecord();
            //    btnRecord.Text = "Pause";
            //}
        }

        protected void OnEncoderSwitch(object sender, EventArgs e)
        {
            // TODO: Possibly hook this up later
        }

        protected override void OnResume()
        {
            base.OnResume();

            btnPublish.Enabled = true;
            //mPublisher.ResumeRecord();
        }

        protected override void OnPause()
        {
            base.OnPause();

            mPublisher.PauseRecord();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            btnPublish.Click -= OnPublishClick;
            btnSwitchCamera.Click -= OnCameraSwitch;
            //btnRecord.Click -= OnRecordClick;

            mPublisher.StopPublish();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            mPublisher.StopEncode();

            mPublisher.SetScreenOrientation((int) newConfig.Orientation);

            if (btnPublish.Text == "Stop")
            {
                mPublisher.StartEncode();
            }

            mPublisher.StartCamera();
        }

        void ShowShortToast(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        void HandleException(Java.Lang.Exception e)
        {
            try
            {
                ShowShortToast(e.Message);

                mPublisher.StopPublish();
                mPublisher.StopRecord();

                btnPublish.Text = "Publish";
                //btnRecord.Text = "Record";
                //btnSwitchEncoder.Enabled = true;
            }
            catch
            {
            }
        }

        public void OnRtmpConnecting(string message) => ShowShortToast(message);

        public void OnRtmpConnected(string message) => ShowShortToast(message);

        public void OnRtmpVideoStreaming() { }

        public void OnRtmpAudioStreaming() { }

        public void OnRtmpStopped() => ShowShortToast("Stopped");

        public void OnRtmpDisconnected() => ShowShortToast("Disconnected");

        public void OnRtmpVideoFpsChanged(double fps)
        {
            System.Console.WriteLine($"Output Fps: {fps}");
        }

        public void OnRtmpVideoBitrateChanged(double bitrate)
        {
            int rate = (int) bitrate;

            if ((rate / 1000) > 0)
                System.Console.WriteLine($"Audio bitrate: {bitrate / 1000} kbps");
            else
                System.Console.WriteLine($"Audio bitrate: {rate} bps");
        }

        public void OnRtmpAudioBitrateChanged(double bitrate)
        {
            int rate = (int) bitrate;

            if ((rate / 1000) > 0)
                System.Console.WriteLine($"Video bitrate: {bitrate / 1000} kbps");
            else
                System.Console.WriteLine($"Video bitrate: {rate} bps");
        }

        public void OnRtmpSocketException(SocketException e) => HandleException(e);

        public void OnRtmpIOException(IOException e) => HandleException(e);

        public void OnRtmpIllegalArgumentException(IllegalArgumentException e) => HandleException(e);

        public void OnRtmpIllegalStateException(IllegalStateException e) => HandleException(e);

        public void OnRecordPause() => ShowShortToast("Record paused");

        public void OnRecordResume() => ShowShortToast("Record resumed");

        public void OnRecordStarted(string msg) => ShowShortToast("Recording file: " + msg);

        public void OnRecordFinished(string msg) => ShowShortToast("MP4 file saved: " + msg);

        public void OnRecordIOException(IOException e) => HandleException(e);

        public void OnRecordIllegalArgumentException(IllegalArgumentException e) => HandleException(e);

        // Implementation of SrsEncodeHandler

        public void OnNetworkWeak() => ShowShortToast("Network weak");

        public void OnNetworkResume() => ShowShortToast("Network resume");

        public void OnEncodeIllegalArgumentException(IllegalArgumentException e) => HandleException(e);
    }
}