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
using WLib.Core.Mobile.Andriod.Services.Streaming;
using WLib.Core.Mobile.Services.AppServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DroidStreamingService))]

namespace WLib.Core.Mobile.Andriod.Services.Streaming
{
    public class DroidStreamingService : IYaseaaStreaming
    {
        public void ShowStreamingWindow()
        {
            //var intent = new Intent(_context, typeof(Test));
            //Forms.Context.StartActivity(intent);
            var context = Android.App.Application.Context;
            var intent = new Intent(context, typeof(FullScreenStreamingActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
    }
}