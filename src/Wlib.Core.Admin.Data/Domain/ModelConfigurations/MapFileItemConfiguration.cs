using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Entities;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class MapFileItemConfiguration : IEntityTypeConfiguration<FileItemEntity>
    {
        public void Configure(EntityTypeBuilder<FileItemEntity> builder)
        {
            builder.ToTable("FileItem");
        }
    }
}
