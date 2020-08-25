using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class StoryPageConfiguration : IEntityTypeConfiguration<StoryPageEntity>
    {
        public void Configure(EntityTypeBuilder<StoryPageEntity> builder)
        {
            builder.ToTable("StoryPage");
            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode(true).IsRequired(false);
        }
    }
}
