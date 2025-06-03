using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers;

public class MovieController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}