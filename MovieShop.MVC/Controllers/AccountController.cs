using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Entities;

namespace MovieShop.MVC.Controllers;

public class AccountController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<Role> roleManager)
    : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError(string.Empty, "Email and password are required");
            return View();
        }

        // Check if a user exists and is in the Admin role
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
        if (!isAdmin)
        {
            ModelState.AddModelError(string.Empty, "Only administrators can log in.");
            return View();
        }

        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, true);
        if (result.Succeeded) return RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(string email, string password, string confirmPassword, string firstName,
        string lastName)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(firstName) ||
            string.IsNullOrEmpty(lastName))
        {
            ModelState.AddModelError(string.Empty, "All fields are required");
            return View();
        }

        if (password != confirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Password and confirmation password do not match");
            return View();
        }

        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = DateTime.Now // Default value can be updated later
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            // Check if the Admin role exists, if not create it
            if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new Role { Name = "Admin" });

            // Assign an Admin role to the user
            await userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Login");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    // Special method to create the first admin - will only work when no admins exist
    [HttpGet]
    public async Task<IActionResult> SetupFirstAdmin()
    {
        // Check if any admin users exist
        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        if (adminUsers.Any()) return RedirectToAction("AccessDenied");

        return View("FirstAdmin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetupFirstAdmin(string email, string password, string confirmPassword,
        string firstName, string lastName)
    {
        // Check if any admin users exist
        if (!await roleManager.RoleExistsAsync("Admin")) await roleManager.CreateAsync(new Role { Name = "Admin" });

        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        if (adminUsers.Any()) return RedirectToAction("AccessDenied");

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(firstName) ||
            string.IsNullOrEmpty(lastName))
        {
            ModelState.AddModelError(string.Empty, "All fields are required");
            return View("FirstAdmin");
        }

        if (password != confirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Password and confirmation password do not match");
            return View("FirstAdmin");
        }

        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = DateTime.Now // Default value, can be updated later
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");

            // Auto-login the first admin
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

        return View("FirstAdmin");
    }
}