using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Mobile.Services.Locator
{
    public interface IDependencyResolver
    {
        T Resolve<T>(string key = null);
    }
}
