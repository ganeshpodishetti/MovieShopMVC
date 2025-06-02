using Microsoft.AspNetCore.Identity;

namespace MovieShop.Core.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsLocked { get; set; }

    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Review> Reviews { get; set; }

    public ICollection<Purchase> Purchases { get; set; }
    // public ICollection<UserRole> UserRoles { get; set; }
}