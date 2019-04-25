using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Data.Domain.Entities
{
    public abstract class BaseEntity : IEntity<int>, IEquatable<BaseEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return object.Equals((object)x, (object)y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals((object)null, obj))
                return false;
            if (object.ReferenceEquals((object)this, obj))
                return true;
            if (obj.GetType() != typeof(BaseEntity))
                return false;
            return this.Equals((BaseEntity)obj);
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (object.ReferenceEquals((object)null, (object)other))
                return false;
            if (object.ReferenceEquals((object)this, (object)other))
                return true;
            if (other.GetType() != this.GetType())
                return false;
            return other.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            if (object.Equals((object)this.Id, (object)0))
                return base.GetHashCode();
            return this.Id.GetHashCode();
        }
    }
}