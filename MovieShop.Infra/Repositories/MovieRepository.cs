using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Infra.Contracts;
using MovieShop.Infra.Data;

namespace MovieShop.Infra.Repositories;

public class MovieRepository(MovieShopDbContext dbContext) : BaseRepository<Movie>(dbContext), IMovieRepository
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
}