namespace WebApplication2.Models;

public class Pizza : Product
{   
    public string size { get; set; }
    public string type { get; set; } //eg tomato/cheese sauce, vegetarian, all dressed, etc
    public string description { get; set; }
}