using System;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Bll.DataAccess.Model
{
    public abstract class AuditBaseEntity : BaseEntity, IAuditable
    {
        public abstract DateTimeOffset ChangeDate { get; set; }

        public abstract int ChangeUserId { get; set; }

        public abstract DateTimeOffset CreateDate { get; set; }

        public abstract int CreateUserId { get; set; }
    }
}
