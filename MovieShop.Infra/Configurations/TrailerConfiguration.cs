using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
{
    public void Configure(EntityTypeBuilder<Trailer> builder)
    {
        builder.ToTable("Trailers");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasMaxLength(2084);
        builder.Property(e => e.TrailerUrl).HasMaxLength(2084);

        builder.HasOne(e => e.Movie)
              .WithMany(m => m.Trailers)
              .HasForeignKey(e => e.MovieId);
    }
}