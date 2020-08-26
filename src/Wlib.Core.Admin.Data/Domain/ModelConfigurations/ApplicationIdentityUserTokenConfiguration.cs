using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wlib.Core.Admin.Data.Domain.ModelConfigurations
{
    public class ApplicationIdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> modelBuilder)
        {
            //base.Configure(modelBuilder);
            modelBuilder.ToTable("IdentityUserToken");

            modelBuilder.Property(t => t.UserId).HasMaxLength(450).IsUnicode(true);
            modelBuilder.Property(t => t.LoginProvider).HasMaxLength(40).IsUnicode(false);
            modelBuilder.Property(t => t.Name).HasMaxLength(40).IsUnicode(false);
            //modelBuilder.Property(t => t.Content).HasMaxLength(5000).IsUnicode(true).IsRequired();
            //modelBuilder.Property(t => t.UniqueCode).HasMaxLength(50).IsUnicode(false).IsRequired(false);
            //modelBuilder.Property(t => t.GuidId).HasMaxLength(50).IsUnicode(true).IsRequired(false);
        }
    }
}