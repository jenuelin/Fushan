using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataServices.EntityTypeConfigurations
{
    internal sealed class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Department).WithMany(m => m.AppUsers).OnDelete(DeleteBehavior.Cascade);
            //builder.Property(m => m.Identity.LastName).HasMaxLength(256);
            //builder.Property(m => m.Identity.FirstName).HasMaxLength(256);
            //builder.HasMany(m => m.Games);
        }
    }

}
