using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WLib.Core.Bll.DataAccess.SQLManagement
{
    public interface IDirectSQLConnection
    {
        IDbConnection DbConnection { get; }
        void Open();
        void Close();
    }
}
