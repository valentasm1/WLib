using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLogEntity>
    {
        public void Configure(EntityTypeBuilder<EventLogEntity> builder)
        {
            builder.ToTable("EventLog");
            builder.Property(x => x.ObjectId).HasMaxLength(1000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.ObjectName).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.Event).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.Message).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.MessageAdditional).HasMaxLength(10000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.SessionID).HasMaxLength(1000).IsUnicode(true).IsRequired(false);
            builder.Property(x => x.User).HasMaxLength(4000).IsUnicode(true).IsRequired(false);
        }
    }
}
