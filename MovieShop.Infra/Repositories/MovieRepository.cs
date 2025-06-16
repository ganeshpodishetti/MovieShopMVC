using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Infra.Contracts;
using MovieShop.Infra.Data;

namespace MovieShop.Infra.Repositories;

public class MovieRepository(MovieShopDbContext dbContext) :
    BaseRepository<Movie>(dbContext),
    IMovieRepository
{
    private readonly MovieShopDbContext _dbContext = dbContext;

    public IEnumerable<Movie> GetTop20GrossingMovies()
    {
        var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue)
                               .Take(20)
                               .AsNoTracking()
                               .ToList();
        return movies;
    }

    public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
    {
        var movies = await _dbContext.Movies
                                     .Where(m => m.MovieGenres.Any(g => g.Genre.Name == genre))
                                     .AsNoTracking()
                                     .ToListAsync();
        return movies;
    }
}