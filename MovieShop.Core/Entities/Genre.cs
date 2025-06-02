namespace MovieShop.Core.Entities;

public class Genre : Base
{
    public string Name { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
}