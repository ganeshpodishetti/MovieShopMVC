namespace MovieShop.Core.Entities;

public class Cast : Base
{
    public string Gender { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ProfilePath { get; set; } = null!;
    public int TmdbUrl { get; set; }

    public ICollection<MovieCast> MovieCasts { get; set; } = [];
}