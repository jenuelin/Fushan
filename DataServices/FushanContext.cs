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

        public DbSet<Member> Members { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "User", NormalizedName = "User" }
                );
            //modelBuilder.Entity<AppUser>().HasData(
            //    new AppUser { Id = Guid.NewGuid(), UserName = "admin@gmail.com", Email = "admin@gmail.com", NormalizedUserName = "admin" }
            //    );
            modelBuilder.Entity<Member>();
            modelBuilder.Entity<Game>();
            modelBuilder.Entity<Department>();

            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new MemberMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
        }
    }
}