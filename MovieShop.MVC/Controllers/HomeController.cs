using Microsoft.AspNetCore.Mvc;
using MovieShop.Services.Contracts.Services;

namespace MovieShop.MVC.Controllers;

public class HomeController(IMovieService movieService) : Controller
{
    public IActionResult Index()
    {
        var movies = movieService.GetTop20MoviesAsync();
        return View(movies);
    }
}