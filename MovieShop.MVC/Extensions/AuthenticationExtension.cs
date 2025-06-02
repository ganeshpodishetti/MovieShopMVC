using Microsoft.AspNetCore.Identity;
using MovieShop.Core.Entities;
using MovieShop.Infra.Data;

namespace MovieShop.MVC.Extensions;

public static class AuthenticationExtension
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<MovieShopDbContext>()
               .AddDefaultTokenProviders();
    }
}