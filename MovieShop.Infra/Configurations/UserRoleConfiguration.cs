using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infra.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        // builder.ToTable("UserRoles");
        // builder.HasKey(e => new { e.RoleId, e.UserId });
        //
        // builder.HasOne(ur => ur.User)
        //       .WithMany(u => u.UserRoles)
        //       .HasForeignKey(ur => ur.UserId);
        //
        // builder.HasOne(ur => ur.Role)
        //       .WithMany(r => r.UserRoles)
        //       .HasForeignKey(ur => ur.RoleId);
    }
}