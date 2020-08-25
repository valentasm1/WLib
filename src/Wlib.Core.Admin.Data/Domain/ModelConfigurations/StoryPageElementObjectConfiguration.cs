using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class StoryPageElementObjectConfiguration : IEntityTypeConfiguration<StoryPageElementObjectEntity>
    {
        public void Configure(EntityTypeBuilder<StoryPageElementObjectEntity> builder)
        {
            builder.ToTable("StoryPageElementObject");
            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode(true).IsRequired(false);
        }
    }
}
