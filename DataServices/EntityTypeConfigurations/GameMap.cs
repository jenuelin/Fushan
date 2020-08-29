using DataServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataServices.EntityTypeConfigurations
{
    internal sealed class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).IsRequired().ValueGeneratedNever();
            builder.Property(g => g.Name).HasMaxLength(256);
            builder.Property(g => g.Code).HasMaxLength(256);
        }
    }

}
