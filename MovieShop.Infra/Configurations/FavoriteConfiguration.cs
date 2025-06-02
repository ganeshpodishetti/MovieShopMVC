using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorites");
        builder.HasKey(f => new { f.MovieId, f.UserId });

        builder.HasOne(f => f.Movie)
              .WithMany(m => m.Favorites)
              .HasForeignKey(f => f.MovieId);

        builder.HasOne(f => f.User)
              .WithMany(u => u.Favorites)
              .HasForeignKey(f => f.UserId);
    }
}