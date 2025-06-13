using Microsoft.AspNetCore.Identity;
using MovieShop.Core.Entities;
using MovieShop.Infra.Data;

namespace MovieShop.MVC.Extensions;

public static class AuthenticationExtension
{
    public static void AddAuthentication(this IServiceCollection service)
    {
        service.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<MovieShopDbContext>()
               .AddDefaultTokenProviders();
    }
}