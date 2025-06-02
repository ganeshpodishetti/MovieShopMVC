namespace MovieShop.Core.Entities;

public class Purchase
{
    public int UserId { get; set; }
    public int MovieId { get; set; }

    public DateTime PurchaseDateTime { get; set; }
    public Guid PurchaseNumber { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation Properties
    public Movie Movie { get; set; }
    public User User { get; set; }
}