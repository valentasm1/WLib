using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Services.Web
{
    public interface IGrpcClientConfig
    {
        int Port { get; }
        string ServerUrl { get; }
    }
}
