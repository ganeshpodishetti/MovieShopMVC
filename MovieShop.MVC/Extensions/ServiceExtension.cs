using MovieShop.Infra.Contracts;
using MovieShop.Infra.Repositories;
using MovieShop.Services.Contracts.Services;
using MovieShop.Services.Services;

namespace MovieShop.MVC.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        return services;
    }
}