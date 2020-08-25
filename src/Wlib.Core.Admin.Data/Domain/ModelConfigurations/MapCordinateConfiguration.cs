using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kosmos.Core.Data.Domain.ModelConfigurations
{
    public class MapCordinateConfiguration : IEntityTypeConfiguration<CoordinateEntity>
    {
        public void Configure(EntityTypeBuilder<CoordinateEntity> builder)
        {
            builder.ToTable("Coordinate");

        }
    }
}
