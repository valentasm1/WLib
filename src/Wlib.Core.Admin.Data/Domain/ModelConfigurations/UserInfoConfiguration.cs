using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfoEntity>
    {
        public void Configure(EntityTypeBuilder<UserInfoEntity> builder)
        {
            builder.ToTable("UserInfo");
            builder.Property(x => x.ObjectId).HasMaxLength(1000).IsUnicode(true).IsRequired(false);
            //builder.Property(x => x.ObjectName).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
            //builder.Property(x => x.Event).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
            //builder.Property(x => x.Message).IsUnicode(true).IsRequired(false);
            //builder.Property(x => x.MessageAdditional).HasMaxLength(10000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.Message).HasMaxLength(1000).IsUnicode(true).IsRequired(false);
            //builder.Property(x => x.User).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
        }
    }
}
