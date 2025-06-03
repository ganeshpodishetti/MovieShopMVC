namespace MovieShop.Services.Dtos.Movie;

public class MovieDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterURL { get; set; }
    public string? Overview { get; set; }
    public string? TagLine { get; set; }
    public decimal Budget { get; set; }
    public decimal Revenue { get; set; }
}