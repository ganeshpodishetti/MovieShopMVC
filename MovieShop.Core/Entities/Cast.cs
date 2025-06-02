namespace MovieShop.Core.Entities;

public class Cast : Base
{
    public string Gender { get; set; }
    public string Name { get; set; }
    public string ProfilePath { get; set; }
    public int TmdbUrl { get; set; }

    public ICollection<MovieCast> MovieCasts { get; set; }
}