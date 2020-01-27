using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.DataAccess.SQLManagement
{
    public interface IDBConnectionSettings
    {

        string DbServerName { get; set; }

        int? DbServerPort { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        string DbName { get; set; }
    }
}
