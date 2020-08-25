using System;
using WLib.Core.Bll.Model.Meta;

namespace Wlib.Core.Admin.Data.Domain.Entities
{
    public class ApplicationEntityBase : IDeletable, IEntity, IAuditEntity
    {
        public virtual bool Active { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual int Id { get; set; }

        public DateTimeOffset ChangeDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

    }


}
