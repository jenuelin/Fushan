using DataServices.EntityTypeConfigurations;
using DataServices.Model;
using Microsoft.EntityFrameworkCore;

namespace Fushan.EntityFrameworkCore
{
    public class FushanContext : DbContext
    {
        public FushanContext(DbContextOptions<FushanContext> options) : base(options) { }
        public DbSet<User> Members { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Game>();

            modelBuilder.ApplyConfiguration(new MemberMap());
            modelBuilder.ApplyConfiguration(new GameMap());
        }
    }
}
