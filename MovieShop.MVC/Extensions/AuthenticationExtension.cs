using Microsoft.AspNetCore.Identity;
using MovieShop.Core.Entities;
using MovieShop.Infra.Data;

namespace MovieShop.MVC.Extensions;

public static class AuthenticationExtension
{
    public static void AddAuthentication(this IServiceCollection service)
    {
        service.AddIdentity<User, Role>(options =>
               {
                   // Password settings
                   options.Password.RequireDigit = true;
                   options.Password.RequireLowercase = true;
                   options.Password.RequireUppercase = true;
                   options.Password.RequireNonAlphanumeric = true;
                   options.Password.RequiredLength = 8;

                   // Lockout settings
                   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                   options.Lockout.MaxFailedAccessAttempts = 5;

                   // User settings
                   options.User.RequireUniqueEmail = true;
               })
               .AddEntityFrameworkStores<MovieShopDbContext>()
               .AddDefaultTokenProviders();

        // Configure cookie settings
        service.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(24);
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }
}