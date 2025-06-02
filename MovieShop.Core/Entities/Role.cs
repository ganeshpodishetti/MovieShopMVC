namespace MovieShop.Core.Entities;

public class Role : Base
{
     public required string Name { get; set; }

     // Navigation Properties
     public ICollection<UserRole> UserRoles { get; set; }
}