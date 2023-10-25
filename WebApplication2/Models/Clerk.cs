namespace WebApplication2.Models;

public class Clerk : Employee
{
    public double NumberOrders => Orders.Count;
}