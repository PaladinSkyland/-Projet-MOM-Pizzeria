namespace WebApplication2.Models;

public class Order
{
    public int Id { get; set; }
    public required Customer Customer { get; set; }
    public required Clerk Clerk { get; set; }
    public Deliverer? Deliverer { get; set; }
    public required string OrderDate { get; set; }
    public required string OrderStatus { get; set; }
    public ICollection<OrderRow> OrdersRows { get; set; } = new List<OrderRow>();

    public double Price
    {
        get
        {
            return OrdersRows.Sum(orderRow => orderRow.Quantity * orderRow.Product.Price);
        }
    }

}