namespace WebApplication2.Models;

public class Deliverer : Employee
{
    public required string Vehicle { get; set; }
    public required int NumberDeliveries { get; set; }
}