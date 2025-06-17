using MovieShop.Core.Entities;
using MovieShop.Infra.Contracts;
using MovieShop.Services.Dtos.Movie;

namespace MovieShop.Services.Contracts.Services;

public interface IMovieService : IRepository<Movie>
{
    IEnumerable<MovieCardDto> GetTop20MoviesAsync();
    Task<IEnumerable<MovieCardDto>> GetMoviesByGenreAsync(string genre, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync(int pageNumber = 1, int pageSize = 10);
    Task<int> GetMoviesCountAsync();
    Task<int> GetMoviesByGenreCountAsync(string genre);
    Task<MovieDetailsDto> GetMovieDetailsAsync(int id);
    Task<IEnumerable<MovieCardDto>> SearchMoviesAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);
    Task<int> GetSearchMoviesCountAsync(string searchTerm);
}