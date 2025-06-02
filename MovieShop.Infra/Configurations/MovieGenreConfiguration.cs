using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenres");
        builder.HasKey(e => new { e.MovieId, e.GenreId });

        builder.HasOne(e => e.Movie)
              .WithMany(m => m.MovieGenres)
              .HasForeignKey(e => e.MovieId);

        builder.HasOne(e => e.Genre)
              .WithMany(g => g.MovieGenres)
              .HasForeignKey(e => e.GenreId);
    }
}