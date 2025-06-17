namespace MovieShop.Core.Entities;

public class Review
{
    public DateTime CreatedDate { get; set; }
    public decimal Rating { get; set; }
    public string? ReviewText { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}