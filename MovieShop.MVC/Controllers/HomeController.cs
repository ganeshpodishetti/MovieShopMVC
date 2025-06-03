using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieShop.MVC.Models;
using MovieShop.Services.Contracts.Services;

namespace MovieShop.MVC.Controllers;

public class HomeController(IMovieService movieService) : Controller
{
    public IActionResult Index()
    {
        var movies = movieService.GetTop20MoviesAsync();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}