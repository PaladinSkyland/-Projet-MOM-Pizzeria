namespace WebApplication2.Models;

public class Order
{
    public int Id { get; set; }
    public required Customer Customer { get; set; }
    public required Clerk Clerk { get; set; }
    public Deliverer? Deliverer { get; set; }
    public required string OrderDate { get; set; }
    public required string OrderStatus { get; set; }
}