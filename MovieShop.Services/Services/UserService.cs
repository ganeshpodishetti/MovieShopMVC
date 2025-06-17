using Microsoft.AspNetCore.Identity;
using MovieShop.Core.Entities;
using MovieShop.Services.Contracts.Services;
using MovieShop.Services.Dtos.User;

namespace MovieShop.Services.Services;

public class UserService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    RoleManager<Role> roleManager)
    : IUserService
{
    public async Task<bool> RegisterUserAsync(RegisterDto model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return false;
        // Check if the Admin role exists, if not create it
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new Role { Name = "Admin" });
        await userManager.AddToRoleAsync(user, "Admin");

        return true;
    }

    public async Task<bool> LoginAsync(LoginDto model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null) return false;

        if (!await IsAdminAsync(model.Email)) return false;

        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> IsAdminAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return false;

        return await userManager.IsInRoleAsync(user, "Admin");
    }
}