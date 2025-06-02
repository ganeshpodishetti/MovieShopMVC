using Microsoft.AspNetCore.Identity;

namespace MovieShop.Core.Entities;

public class Role : IdentityRole<int>
{
    // Default constructor required by EF Core
    public Role()
    {
    }

    // Constructor with name parameter
    public Role(string roleName) : base(roleName)
    {
    }
}