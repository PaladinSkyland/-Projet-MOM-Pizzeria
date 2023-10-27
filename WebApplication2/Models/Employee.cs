namespace WebApplication2.Models;

public abstract class Employee : People
{
    public required string JobTitle { get; set; }
    public required string Gender { get; set; }
    public required DateOnly HireDate { get; set; }
    public required double Salary { get; set; }
}