using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WLib.Core.Mobile.Andriod.Services.Streaming
{
    [Activity]
    public class FullScreenStreamingActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string rtmpUrl = "rtmp://62.77.152.170:1935/live/test";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StreamingLayout);
        }
    }
}