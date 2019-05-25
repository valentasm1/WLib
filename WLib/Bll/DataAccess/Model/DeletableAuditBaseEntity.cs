using System;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Bll.DataAccess.Model
{
    public abstract class DeletableAuditBaseEntity : AuditBaseEntity, IDeletable
    {
        public abstract override DateTimeOffset ChangeDate { get; set; }
        public abstract override int ChangeUserId { get; set; }
        public abstract override DateTimeOffset CreateDate { get; set; }
        public abstract override int CreateUserId { get; set; }
        public abstract bool Deleted { get; set; }
    }
}

