using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataServices.EntityTypeConfigurations
{
    internal sealed class MemberMap : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            //builder.Property(m => m.Identity.LastName).HasMaxLength(256);
            //builder.Property(m => m.Identity.FirstName).HasMaxLength(256);
            //builder.HasMany(m => m.Games);
        }
    }

}
