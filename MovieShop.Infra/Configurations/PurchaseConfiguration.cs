using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");
        builder.HasKey(e => new { e.MovieId, e.UserId });

        builder.Property(e => e.TotalPrice).HasColumnType("decimal(5,2)");

        builder.HasOne(p => p.Movie)
              .WithMany(m => m.Purchases)
              .HasForeignKey(p => p.MovieId);

        builder.HasOne(p => p.User)
              .WithMany(u => u.Purchases)
              .HasForeignKey(p => p.UserId);
    }
}