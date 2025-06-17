using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Infra.Data;
using MovieShop.Services.Contracts.Services;
using MovieShop.Services.Dtos.Movie;

namespace MovieShop.Services.Services;

public class FavoriteService(MovieShopDbContext dbContext, IMovieService movieService) : IFavoriteService
{
    private readonly IMovieService _movieService = movieService;

    public async Task<bool> AddFavoriteAsync(int movieId, int userId)
    {
        // Check if already a favorite
        var existingFavorite = await dbContext.Favorites
                                              .FirstOrDefaultAsync(f => f.MovieId == movieId && f.UserId == userId);

        if (existingFavorite != null)
            return true; // Already a favorite

        var favorite = new Favorite
        {
            MovieId = movieId,
            UserId = userId
        };

        await dbContext.Favorites.AddAsync(favorite);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> RemoveFavoriteAsync(int favoriteId)
    {
        var favorite = await dbContext.Favorites.FindAsync(favoriteId);
        if (favorite == null)
            return false;

        dbContext.Favorites.Remove(favorite);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IEnumerable<MovieCardDto>> GetUserFavoritesAsync(int userId)
    {
        var favorites = await dbContext.Favorites
                                       .Include(f => f.Movie)
                                       .Where(f => f.UserId == userId)
                                       .OrderByDescending(f => f.MovieId)
                                       .ToListAsync();

        var favoritesDto = favorites.Select(f => new MovieCardDto
        {
            Id = f.Movie.Id,
            Title = f.Movie.Title,
            PosterURL = f.Movie.PosterUrl,
            FavoriteId = f.MovieId // Include the favorite ID for removal functionality
        });

        return favoritesDto;
    }

    public async Task<bool> IsFavoriteAsync(int movieId, int userId)
    {
        return await dbContext.Favorites
                              .AnyAsync(f => f.MovieId == movieId && f.UserId == userId);
    }
}