using Android.App;
using Android.Widget;
using WLib.Core.Mobile.Andriod.Services.UiServices;
using WLib.Core.Mobile.Services.AppServices;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace WLib.Core.Mobile.Andriod.Services.UiServices
{
    public class NotificationService : INotificationService
    {
        public void ShowToast(string message, int durationInSeconds = 2)
        {
            if (durationInSeconds <= 0)
            {
                durationInSeconds = 2;
            }

            for (int i = 0; i < durationInSeconds; i++)
            {
                Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
            }


        }
    }
}