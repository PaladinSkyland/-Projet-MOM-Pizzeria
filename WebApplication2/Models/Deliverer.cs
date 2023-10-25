namespace WebApplication2.Models;

public class Deliverer : Employee
{
    public required string Vehicle { get; set; }
    public int NumberDeliveries => Orders.Count;
}