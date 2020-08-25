using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class MapObjectConfiguration : IEntityTypeConfiguration<MapObjectEntity>
    {
        public void Configure(EntityTypeBuilder<MapObjectEntity> builder)
        {
            builder.ToTable("MapObject");
            builder.Property(x => x.Name).HasMaxLength(1000).IsUnicode(true).IsRequired(true);
            builder.Property(x => x.Color).HasMaxLength(50).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.ColorPassive).HasMaxLength(50).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.Description).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.TranslationKey).IsUnicode(false).HasMaxLength(250).IsRequired(false);
        }
    }
}
