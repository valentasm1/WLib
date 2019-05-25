using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface IOrderable
    {
        int? OrderBy { get; set; }
    }
}
