using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wlib.Core.Admin.Data.Domain.Authentication;

namespace Wlib.Core.Admin.Data.Domain.ModelConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> modelBuilder)
        {
            //base.Configure(modelBuilder);
            modelBuilder.ToTable("AspNetUsers");

            //NormalizedUserName
            //    ApplicationUserType
            //ConcurrencyStamp
            //    LockoutEnd
            //NormalizedEmail

            //modelBuilder.Ignore(t => t.NormalizedUserName);
            //modelBuilder.Ignore(t => t.ApplicationUserType);
            //modelBuilder.Ignore(t => t.ConcurrencyStamp);
            //modelBuilder.Ignore(t => t.LockoutEnd);
            //modelBuilder.Ignore(t => t.NormalizedEmail);

            //modelBuilder.Property(t => t.Name).HasMaxLength(50).IsUnicode(true).IsRequired();
            //modelBuilder.Property(t => t.Content).HasMaxLength(5000).IsUnicode(true).IsRequired();
            //modelBuilder.Property(t => t.UniqueCode).HasMaxLength(50).IsUnicode(false).IsRequired(false);
            //modelBuilder.Property(t => t.GuidId).HasMaxLength(50).IsUnicode(true).IsRequired(false);
        }
    }
}