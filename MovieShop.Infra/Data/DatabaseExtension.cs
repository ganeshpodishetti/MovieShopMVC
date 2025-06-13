using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MovieShop.Infra.Data;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        // Add DbContext
        services.AddDbContext<MovieShopDbContext>((provider, options) =>
        {
            var connectionString = provider.GetRequiredService<IOptions<ConnectionString>>().Value.MovieShopDb;

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection string is not set");

            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                sqlServerOptions.CommandTimeout(60);
                sqlServerOptions.MigrationsAssembly("MovieShop.Infra");
            });
        });

        return services;
    }
}