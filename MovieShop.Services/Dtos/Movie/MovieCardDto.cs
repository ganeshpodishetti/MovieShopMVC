namespace MovieShop.Services.Dtos.Movie;

public class MovieCardDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!; // Ensure Title is not null
    public string PosterURL { get; set; } = null!; // Ensure PosterURL is not null
    public int? FavoriteId { get; set; } // Added to support favorite management
}