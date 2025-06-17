using Microsoft.AspNetCore.Mvc;
using MovieShop.Services.Contracts.Services;

namespace MovieShop.MVC.Controllers;

public class MovieController(IMovieService movieService) : Controller
{
    private const int PageSize = 10;

    // GET
    public async Task<IActionResult> MoviesByGenre(string genre, int page = 1)
    {
        // Ensure page is at least 1
        if (page < 1) page = 1;

        // Pass the genre name to the view using ViewBag
        ViewBag.Genre = genre;
        ViewBag.CurrentPage = page;

        // Get total count for pagination
        var totalMovies = await movieService.GetMoviesByGenreCountAsync(genre);
        ViewBag.TotalPages = (int)Math.Ceiling(totalMovies / (double)PageSize);

        var movies = await movieService.GetMoviesByGenreAsync(genre, page);
        return View(movies);
    }

    public async Task<IActionResult> AllMovies(int page = 1)
    {
        // Ensure page is at least 1
        if (page < 1) page = 1;

        ViewBag.CurrentPage = page;

        // Get total count for pagination
        var totalMovies = await movieService.GetMoviesCountAsync();
        ViewBag.TotalPages = (int)Math.Ceiling(totalMovies / (double)PageSize);

        var movies = await movieService.GetAllMoviesAsync(page);
        return View(movies);
    }

    public async Task<IActionResult> Details(int id)
    {
        var movieDetails = await movieService.GetMovieDetailsAsync(id);
        if (movieDetails is null) return NotFound();

        return View(movieDetails);
    }

    public async Task<IActionResult> Search(string searchTerm, int page = 1)
    {
        // Ensure page is at least 1
        if (page < 1) page = 1;

        ViewBag.SearchTerm = searchTerm;
        ViewBag.CurrentPage = page;

        // Get total count for pagination
        var totalMovies = await movieService.GetSearchMoviesCountAsync(searchTerm);
        ViewBag.TotalPages = (int)Math.Ceiling(totalMovies / (double)PageSize);

        var movies = await movieService.SearchMoviesAsync(searchTerm, page);
        return View(movies);
    }
}