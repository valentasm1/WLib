using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Mobile.Services.AppServices
{
    public interface INotificationService
    {
        void ShowToast(string message, int durationSecconds = 2);

    }
}
