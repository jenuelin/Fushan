using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataServices.EntityTypeConfigurations
{
    internal sealed class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.DepartmentLeader).WithOne(q => q.Department).HasForeignKey<Department>(ur => ur.DepartmentLeaderId);
            //builder.HasMany(m => m.AppUsers).WithOne().HasForeignKey(ur => ur.Id).OnDelete(DeleteBehavior.Restrict);
            //builder.Property(m => m.Identity.LastName).HasMaxLength(256);
            //builder.Property(m => m.Identity.FirstName).HasMaxLength(256);
            //builder.HasMany(m => m.Games);
        }
    }
}