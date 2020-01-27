using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Mobile.Xf.Services.Auth
{
    public interface IAppUser
    {
        string Name { get; set; }

        string Email { get; set; }

        bool IsLogged { get; set; }


    }
}
