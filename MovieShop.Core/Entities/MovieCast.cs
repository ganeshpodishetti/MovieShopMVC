namespace MovieShop.Core.Entities;

public class MovieCast
{
    public int CastId { get; set; }
    public int MovieId { get; set; }
    public string? Character { get; set; }

    // Navigation Properties
    public Cast Cast { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}