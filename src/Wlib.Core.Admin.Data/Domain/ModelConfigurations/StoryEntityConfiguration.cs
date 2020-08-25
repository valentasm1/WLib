using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    partial class StoryEntityConfiguration : IEntityTypeConfiguration<StoryEntity>
    {
        public void Configure(EntityTypeBuilder<StoryEntity> builder)
        {
            builder.ToTable("Story");
            builder.Property(x => x.Name).HasMaxLength(1000).IsUnicode(true).IsRequired(true);
        }
    }
}
