namespace MovieShop.Core.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? BackdropUrl { get; set; }
    public decimal Budget { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime Createddate { get; set; }
    public string? ImdbUrl { get; set; }
    public string? OriginalLanguage { get; set; }
    public string? Overview { get; set; }
    public string? PosterUrl { get; set; }
    public decimal Price { get; set; }
    public string? ReleaseDate { get; set; }
    public decimal Revenue { get; set; }
    public int Runtime { get; set; }
    public string? Tagline { get; set; }
    public string? TmdbUrl { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }

    // Navigate properties
    public ICollection<MovieGenre> MovieGenres { get; set; }
    public ICollection<MovieCast> MovieCasts { get; set; }
    public ICollection<Trailer> Trailers { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Purchase> Purchases { get; set; }

}