using System;
using DataServices.EntityTypeConfigurations;
using DataServices.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Db
{
    public class FushanContext : IdentityDbContext<AppUser, Role, Guid>
    {
        public FushanContext(DbContextOptions<FushanContext> options) : base(options) { }
        public DbSet<Member> Members { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>();
            modelBuilder.Entity<Game>();
            modelBuilder.Entity<Department>();

            modelBuilder.ApplyConfiguration(new MemberMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
        }
    }
}
