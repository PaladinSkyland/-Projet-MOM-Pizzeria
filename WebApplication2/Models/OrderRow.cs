namespace WebApplication2.Models;

public class OrderRow
{
    public int Id { get; set; }
    public required Order Order { get; set; }
    public required Product Product { get; set; }
    public required int Quantity { get; set; }
}