using System.ComponentModel.DataAnnotations;

namespace MovieShop.Services.Dtos.User;

public class LoginDto
{
    [Required] [EmailAddress] public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public bool RememberMe { get; set; }
}

public class RegisterDto
{
    [Required] [EmailAddress] public required string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required] public required string FirstName { get; set; }

    [Required] public required string LastName { get; set; }
}