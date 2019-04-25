using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
