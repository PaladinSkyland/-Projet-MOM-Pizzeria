namespace WebApplication2.Models;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int ClerkId { get; set; }
    public int DelivererId { get; set; }
    public string OrderDate { get; set; }
    public string OrderStatus { get; set; }
}