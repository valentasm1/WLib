using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<SettingEntity>
    {
        public void Configure(EntityTypeBuilder<SettingEntity> builder)
        {
            builder.ToTable("Setting");
            builder.Property(x => x.Name).HasMaxLength(200).IsUnicode(true).IsRequired(true);
            builder.Property(x => x.UniqueCode).HasMaxLength(200).IsUnicode(false).IsRequired(false);
            builder.Property(x => x.Value).HasMaxLength(10000).IsUnicode(true).IsRequired(true);
        }
    }
}
