using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Entities;
using MovieShop.Services.Contracts.Services;

namespace MovieShop.MVC.Controllers;

[Authorize(Roles = "Admin")]
public class FavoriteController(IFavoriteService favoriteService, UserManager<User> userManager)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");

        var favorites = await favoriteService.GetUserFavoritesAsync(user.Id);
        return View(favorites);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int movieId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");

        await favoriteService.AddFavoriteAsync(movieId, user.Id);

        // If the request is AJAX, return a partial result
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return Json(new { success = true });

        // Otherwise redirect back to the movie details
        return RedirectToAction("Details", "Movie", new { id = movieId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int favoriteId, string returnUrl = null)
    {
        await favoriteService.RemoveFavoriteAsync(favoriteId);

        // If the request is AJAX, return a partial result
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return Json(new { success = true });

        // If returnUrl is provided, redirect there
        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        // Otherwise redirect to favorites list
        return RedirectToAction(nameof(Index));
    }
}