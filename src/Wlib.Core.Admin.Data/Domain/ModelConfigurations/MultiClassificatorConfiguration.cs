using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class MultiClassificatorConfiguration : IEntityTypeConfiguration<MultiClassificatorEntity>
    {
        public void Configure(EntityTypeBuilder<MultiClassificatorEntity> builder)
        {
            builder.ToTable("MultiClassificator");
            builder.Property(x => x.Name).HasMaxLength(1000).IsUnicode(true).IsRequired(true);
            builder.Property(x => x.UniqueCode).HasMaxLength(1000).IsUnicode(false).IsRequired(false);
        }
    }
}
