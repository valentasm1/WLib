using System;
using System.Collections.Generic;
using System.Text;

namespace Wlib.Core.Admin.Data.Domain
{
    public interface IAuditEntity
    {
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset ChangeDate { get; set; }
    }
}
