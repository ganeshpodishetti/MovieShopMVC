using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);

        builder.Property(e => e.Title).HasMaxLength(256);
        builder.Property(e => e.Tagline).HasMaxLength(512);
        builder.Property(e => e.ImdbUrl).HasMaxLength(2084);
        builder.Property(e => e.TmdbUrl).HasMaxLength(2084);
        builder.Property(e => e.PosterUrl).HasMaxLength(2084);
        builder.Property(e => e.BackdropUrl).HasMaxLength(2084);
        builder.Property(e => e.OriginalLanguage).HasMaxLength(64);
        builder.Property(e => e.Overview).HasMaxLength(int.MaxValue);
        builder.Property(e => e.Budget).HasColumnType("decimal(18,4)");
        builder.Property(e => e.Revenue).HasColumnType("decimal(18,4)");
        builder.Property(e => e.Price).HasColumnType("decimal(5,2)");
    }
}