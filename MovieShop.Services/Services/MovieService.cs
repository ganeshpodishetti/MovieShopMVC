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

        return top20Movies
               .Select(movie => new MovieCardDto
               {
                   Id = movie.Id,
                   PosterURL = movie.PosterUrl!,
                   Title = movie.Title
               }).ToList();
    }

    public async Task<IEnumerable<MovieCardDto>> GetMoviesByGenreAsync(string genre, int pageNumber = 1,
        int pageSize = 10)
    {
        var movies = await repository.GetMoviesByGenreAsync(genre);
        return movies
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .Select(movie => new MovieCardDto
               {
                   Id = movie.Id,
                   PosterURL = movie.PosterUrl!,
                   Title = movie.Title
               })
               .ToList();
    }

    public async Task<IEnumerable<MovieCardDto>> GetAllMoviesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var movies = await repository.GetAllAsync();
        return movies
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .Select(movie => new MovieCardDto
               {
                   Id = movie.Id,
                   PosterURL = movie.PosterUrl!,
                   Title = movie.Title
               })
               .ToList();
    }

    public async Task<int> GetMoviesCountAsync()
    {
        var movies = await repository.GetAllAsync();
        return movies.Count();
    }

    public async Task<int> GetMoviesByGenreCountAsync(string genre)
    {
        var movies = await repository.GetMoviesByGenreAsync(genre);
        return movies.Count();
    }

    public async Task<MovieDetailsDto> GetMovieDetailsAsync(int id)
    {
        var movie = await repository.GetByIdAsync(id);
        if (movie == null) return null;

        var movieDetails = new MovieDetailsDto
        {
            Id = movie.Id,
            Title = movie.Title,
            PosterUrl = movie.PosterUrl,
            BackdropUrl = movie.BackdropUrl,
            Budget = movie.Budget,
            Overview = movie.Overview,
            Tagline = movie.Tagline,
            Revenue = movie.Revenue,
            Runtime = movie.Runtime,
            Price = movie.Price,
            ReleaseDate = movie.ReleaseDate,
            ImdbUrl = movie.ImdbUrl,
            OriginalLanguage = movie.OriginalLanguage,
            Genres = new List<string>(),
            Casts = new List<CastDto>()
        };

        // Map genres if available
        if (movie.MovieGenres != null && movie.MovieGenres.Any())
            movieDetails.Genres = movie.MovieGenres
                                       .Where(mg => mg.Genre != null && !string.IsNullOrEmpty(mg.Genre.Name))
                                       .Select(mg => mg.Genre.Name)
                                       .ToList();

        // Map casts if available
        if (movie.MovieCasts != null && movie.MovieCasts.Any())
            movieDetails.Casts = movie.MovieCasts
                                      .Where(mc => mc.Cast is not null)
                                      .OrderBy(mc => mc.Cast.Name)
                                      .Select(mc => new CastDto
                                      {
                                          Id = mc.Cast.Id,
                                          Name = mc.Cast.Name,
                                          Character = mc.Character,
                                          ProfilePath = mc.Cast.ProfilePath
                                      })
                                      .Take(10)
                                      .ToList();

        return movieDetails;
    }

    public async Task<IEnumerable<MovieCardDto>> SearchMoviesAsync(string searchTerm, int pageNumber = 1,
        int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return [];

        var movies = await repository.GetAllAsync();

        var filteredMovies = movies
                             .Where(m =>
                                 m.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                 (!string.IsNullOrEmpty(m.Overview) &&
                                  m.Overview.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                 (!string.IsNullOrEmpty(m.Tagline) &&
                                  m.Tagline.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .Select(movie => new MovieCardDto
                             {
                                 Id = movie.Id,
                                 PosterURL = movie.PosterUrl!,
                                 Title = movie.Title
                             })
                             .ToList();

        return filteredMovies;
    }

    public async Task<int> GetSearchMoviesCountAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return 0;

        var movies = await repository.GetAllAsync();

        var count = movies
            .Count(m =>
                m.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(m.Overview) &&
                 m.Overview.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(m.Tagline) &&
                 m.Tagline.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));

        return count;
    }
}