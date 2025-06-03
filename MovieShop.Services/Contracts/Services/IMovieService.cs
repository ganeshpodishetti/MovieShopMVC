using MovieShop.Core.Entities;
using MovieShop.Infra.Contracts;
using MovieShop.Services.Dtos.Movie;

namespace MovieShop.Services.Contracts.Services;

public interface IMovieService : IRepository<Movie>
{
    IEnumerable<MovieCardDto> GetTop20MoviesAsync();
}