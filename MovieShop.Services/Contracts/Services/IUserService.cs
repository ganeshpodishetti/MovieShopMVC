using MovieShop.Services.Dtos.User;

namespace MovieShop.Services.Contracts.Services;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterDto model);
    Task<bool> LoginAsync(LoginDto model);
    Task LogoutAsync();
    Task<bool> IsAdminAsync(string email);
}