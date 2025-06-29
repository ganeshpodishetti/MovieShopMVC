using MovieShop.Core.Entities;

namespace MovieShop.Infra.Contracts;

public interface IMovieRepository : IRepository<Movie>
{
    IEnumerable<Movie> GetTop20GrossingMovies();
    Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
}