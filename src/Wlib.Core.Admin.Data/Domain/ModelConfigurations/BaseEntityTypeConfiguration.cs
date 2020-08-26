using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Wlib.Core.Admin.Data.Domain.ModelConfigurations
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
        where TBase : ApplicationEntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            //Base Configuration
        }
    }
}