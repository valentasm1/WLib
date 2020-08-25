using Android.Content;
using Wlib.Streaming.Android.Services.Streaming;
using WLib.Core.Mobile.Services.AppServices;

[assembly: Xamarin.Forms.Dependency(typeof(DroidStreamingService))]

namespace Wlib.Streaming.Android.Services.Streaming
{
    public class DroidStreamingService : IYaseaaStreaming
    {
        public void ShowStreamingWindow()
        {
            //var intent = new Intent(_context, typeof(Test));
            //Forms.Context.StartActivity(intent);
            var context = global::Android.App.Application.Context;
            var intent = new Intent(context, typeof(FullScreenStreamingActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
    }
}