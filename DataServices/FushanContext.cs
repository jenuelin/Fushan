using DataServices.EntityTypeConfigurations;
using DataServices.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataServices.Db
{
    public class FushanContext : IdentityDbContext<AppUser, Role, Guid>
    {
        public FushanContext(DbContextOptions<FushanContext> options) : base(options)
        {
        }

        //public DbSet<Member> Members { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public new DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "Admin" },
            //    new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "User" }
            //    );
            //modelBuilder.Entity<AppUser>().HasData(
            //    new AppUser { Id = Guid.NewGuid(), UserName = "admin@gmail.com", Email = "admin@gmail.com", NormalizedUserName = "admin" }
            //    );
            //modelBuilder.Entity<Member>();
            modelBuilder.Entity<AppUser>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Department>();

            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            //modelBuilder.ApplyConfiguration(new MemberMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
        }
    }
}