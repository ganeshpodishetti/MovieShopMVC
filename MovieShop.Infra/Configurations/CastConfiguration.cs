using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class CastConfiguration : IEntityTypeConfiguration<Cast>
{
    public void Configure(EntityTypeBuilder<Cast> builder)
    {
        builder.ToTable("Casts");
        builder.HasKey(c => c.Id);

        builder.Property(e => e.Name).HasMaxLength(128).IsRequired();
        builder.Property(e => e.ProfilePath).HasMaxLength(2084);
        builder.Property(e => e.TmdbUrl).HasMaxLength(int.MaxValue);
    }
}