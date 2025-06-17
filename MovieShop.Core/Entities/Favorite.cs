namespace MovieShop.Core.Entities;

public class Favorite
{
    public int UserId { get; set; }
    public int MovieId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}