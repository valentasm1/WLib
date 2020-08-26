using Android.Content;
using Wlib.Streaming.Android.Services.Streaming;
using WLib.Core.Mobile.Services.AppServices;

[assembly: Xamarin.Forms.Dependency(typeof(DroidStreamingProviderService))]

namespace Wlib.Streaming.Android.Services.Streaming
{
    public class DroidStreamingProviderService : IStreamingProvider
    {
        public void ShowStreamingWindow(string url)
        {
            //var intent = new Intent(_context, typeof(Test));
            //Forms.Context.StartActivity(intent);
            var context = global::Android.App.Application.Context;
            var intent = new Intent(context, typeof(FullScreenStreamingActivity));
            intent.AddFlags(ActivityFlags.NewTask);
            intent.PutExtra("rtmpUrl", url);
            context.StartActivity(intent);
        }
    }
}