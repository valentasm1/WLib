using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Mobile.Services.AppServices
{
    public interface IStreamingProvider
    {
        void ShowStreamingWindow(string streamingUrl);
    }
}