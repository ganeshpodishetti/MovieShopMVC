using MovieShop.Services.Dtos.Movie;

namespace MovieShop.Services.Contracts.Services;

public interface IFavoriteService
{
    Task<bool> AddFavoriteAsync(int movieId, int userId);
    Task<bool> RemoveFavoriteAsync(int favoriteId);
    Task<IEnumerable<MovieCardDto>> GetUserFavoritesAsync(int userId);
    Task<bool> IsFavoriteAsync(int movieId, int userId);
}