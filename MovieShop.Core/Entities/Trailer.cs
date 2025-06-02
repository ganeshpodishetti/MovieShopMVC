namespace MovieShop.Core.Entities;

public class Trailer : Base
{
    public int MovieId { get; set; }
    public string TrailerUrl { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public Movie Movie { get; set; }
}