using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class ReviewsConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(e => new { e.MovieId, e.UserId });

        builder.Property(e => e.Rating).HasColumnType("decimal(3,2)");
        builder.Property(e => e.ReviewText).HasMaxLength(int.MaxValue);

        builder.HasOne(r => r.Movie)
              .WithMany(m => m.Reviews)
              .HasForeignKey(r => r.MovieId);

        builder.HasOne(r => r.User)
              .WithMany(u => u.Reviews)
              .HasForeignKey(r => r.UserId);
    }
}