namespace WebApplication2.Models;

public class Employee : People
{
    public string JobTitle { get; set; }
    public string Gender { get; set; }
    public string HireDate { get; set; }
    public string Salary { get; set; }
}