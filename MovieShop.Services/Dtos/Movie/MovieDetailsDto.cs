namespace MovieShop.Services.Dtos.Movie;

public class MovieDetailsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? PosterUrl { get; set; }
    public string? BackdropUrl { get; set; }
    public decimal? Budget { get; set; }
    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public decimal? Revenue { get; set; }
    public int? Runtime { get; set; }
    public decimal? Price { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? ImdbUrl { get; set; }
    public string? OriginalLanguage { get; set; }
    public List<string>? Genres { get; set; } = [];
    public List<CastDto>? Casts { get; set; } = [];
}

public class CastDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Character { get; set; }
    public string? ProfilePath { get; set; }
}