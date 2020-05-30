using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface ISyncableItem
    {
        int RemoteId { get; set; }
    }
}
