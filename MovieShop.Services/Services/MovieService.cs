using MovieShop.Core.Entities;
using MovieShop.Infra.Contracts;
using MovieShop.Services.Contracts.Services;
using MovieShop.Services.Dtos.Movie;

namespace MovieShop.Services.Services;

public class MovieService(IMovieRepository repository) : IMovieService
{
    public async Task<Movie> InsertAsync(Movie movie)
    {
        return await repository.InsertAsync(movie);
    }

    public async Task<bool> UpdateAsync(Movie movie)
    {
        var movieToUpdate = await repository.GetByIdAsync(movie.Id);
        if (movieToUpdate == null) return false;
        await repository.UpdateAsync(movie);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await repository.GetByIdAsync(id);
        return movie != null;
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        var movie = await repository.GetByIdAsync(id);
        return movie;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public IEnumerable<MovieCardDto> GetTop20MoviesAsync()
    {
        var top20Movies = repository.GetTop20GrossingMovies();

        return top20Movies.Select(movie => new MovieCardDto
            { Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title }).ToList();
    }
}