using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.DB;
using WebApplication2.Models;
namespace WebApplication2.Pages;

public class CaissierModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public List<Product> Products { get; set; }
    public int IdCustomer { get; set; }
    public int IdClerk { get; set; }

    public CaissierModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        // Charger les produits depuis la base de données
        Products = _context.Products.ToList();
        if (Request.Query.ContainsKey("customerid"))
        {
            IdCustomer = int.Parse(Request.Query["customerid"]);
        }
        if (Request.Query.ContainsKey("clerkid"))
        {
            IdClerk = int.Parse(Request.Query["clerkid"]);
        }
    }

    public IActionResult OnPostCommit(int idClerk,int idCustomer,List<int> productsIds, List<int> productsQuantities)
    {
        var customerOrder = _context.Customers.Find(idCustomer);
        var clerkOrder = _context.Clerks.Find(idClerk);
        if (productsIds.Count != productsQuantities.Count || productsIds.Count == 0 || customerOrder == null || clerkOrder == null)
        {
            //return error page
            Console.WriteLine("Error");
            return Page();
        }
        else
        {
            //create new order
            Console.WriteLine(customerOrder);
            Console.WriteLine(customerOrder.Name);
            var newOrder = new Order
            {
                Customer = customerOrder,
                Clerk = clerkOrder,
                OrderDate = "TODAY",
                OrderStatus = "Opened"
            };
            
            for (var i = 0; i < productsIds.Count; i++)
            {
                var product = _context.Products.Find(productsIds[i]);
                if (product == null)
                {
                    //return error page
                    return Page();
                }
                
                //create new order row
                var newOrderRow = new OrderRow
                {
                    Order = newOrder,
                    Product = product,
                    Quantity = productsQuantities[i]
                };
                //save order row
                _context.OrdersRows.Add(newOrderRow);
                Console.WriteLine($"Ordered product n° {productsIds[i]} with quantity {productsQuantities[i]}");
            }
            //save order
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            
            Console.WriteLine(productsIds.Count == productsQuantities.Count);
            Console.WriteLine(productsIds.Count);
            //Return success page
            return Page();
        }
        
    }
}