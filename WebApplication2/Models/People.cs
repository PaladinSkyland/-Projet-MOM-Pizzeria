namespace WebApplication2.Models;

public abstract class People
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}