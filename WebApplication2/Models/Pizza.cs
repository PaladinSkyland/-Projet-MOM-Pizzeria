namespace WebApplication2.Models;

public class Pizza : Product
{   
    public required string Size { get; set; }
    public required string Description { get; set; } //eg tomato/cheese sauce, vegetarian, all dressed, etc
}