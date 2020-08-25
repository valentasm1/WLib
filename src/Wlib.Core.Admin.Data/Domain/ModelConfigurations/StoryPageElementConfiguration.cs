using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class StoryPageElementConfiguration : IEntityTypeConfiguration<StoryPageElementEntity>
    {
        public void Configure(EntityTypeBuilder<StoryPageElementEntity> builder)
        {
            builder.ToTable("StoryPageElement");
            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode(true).IsRequired(false);
        }
    }
}
