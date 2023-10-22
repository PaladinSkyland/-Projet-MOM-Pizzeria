namespace WebApplication2.Models;

public class ProductQuantities
{
    public Order OrderId { get; set; }
    public Product ProductId { get; set; }
    public int Quantity { get; set; }
}