namespace WebApplication2.Models;

public abstract class Product
{   
    public int Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public ICollection<OrderRow> OrdersRows { get; set; } = new List<OrderRow>();
}