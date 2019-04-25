using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface IAuditable
    {
        DateTime ChangeDate { get; set; }

        int ChangeUserId { get; set; }

        DateTime CreateDate { get; set; }

        int CreateUserId { get; set; }
    }
}
