using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class FileConfiguration : BaseEntityTypeConfiguration<FileEntity>
    {
        public override void Configure(EntityTypeBuilder<FileEntity> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("Files");

            modelBuilder.Property(t => t.FileName).HasMaxLength(250).IsUnicode(true).IsRequired(true);
            modelBuilder.Property(t => t.FilePath).HasMaxLength(500).IsUnicode(true).IsRequired(true);
        }
    }
}
