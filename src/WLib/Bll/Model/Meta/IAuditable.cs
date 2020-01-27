using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface IAuditable
    {
        DateTimeOffset ChangeDate { get; set; }

        int ChangeUserId { get; set; }

        DateTimeOffset CreateDate { get; set; }

        int CreateUserId { get; set; }
    }
}
