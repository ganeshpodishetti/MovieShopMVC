using MovieShop.Infra.Data;

namespace MovieShop.MVC.Extensions;

public static class OptionsExtension
{
    public static void AddOptions(this WebApplicationBuilder builder)
    {
        // services.Configure<>()
        builder.Services.Configure<ConnectionString>(
            builder.Configuration.GetSection(ConnectionString.MovieShopDbConnection));
    }
}