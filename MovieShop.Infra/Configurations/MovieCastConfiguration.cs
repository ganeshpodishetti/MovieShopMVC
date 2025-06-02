using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
{
    public void Configure(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCasts");
        builder.HasKey(e => new { e.MovieId, e.CastId, e.Character });

        builder.Property(e => e.Character).HasMaxLength(450);

        builder.HasOne(mc => mc.Movie)
              .WithMany(m => m.MovieCasts)
              .HasForeignKey(mc => mc.MovieId);

        builder.HasOne(mc => mc.Cast)
              .WithMany(c => c.MovieCasts)
              .HasForeignKey(mc => mc.CastId);
    }
}