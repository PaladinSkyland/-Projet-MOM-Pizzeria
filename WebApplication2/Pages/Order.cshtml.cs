using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages;

public class OrderPageModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly JobQueue<KitchenJob> _queue;

    [BindProperty] public List<Product> Products { get; set; } = new();
    public int IdCustomer { get; set; }
    public int IdClerk { get; set; }

    public OrderPageModel(ApplicationDbContext context, JobQueue<KitchenJob> queue)
    {
        _context = context;
        _queue = queue;
    }

    public IActionResult OnGet()
    {
        // Load products from the database
        if (!Request.Query.ContainsKey("customerid") || !Request.Query.ContainsKey("clerkid"))
        {
            return NotFound();
        }
        Products = _context.Products.ToList();
        IdCustomer = int.Parse(Request.Query["customerid"].ToString());
        IdClerk = int.Parse(Request.Query["clerkid"].ToString());
        return Page();
    }

    public async Task<IActionResult> OnPostCommitAsync(int idClerk,int idCustomer,List<int> productsIds, List<int> productsQuantities)
    {
        var customerOrder = await _context.Customers.FindAsync(idCustomer);
        var clerkOrder = await _context.Clerks.FindAsync(idClerk);
        if (productsIds.Count != productsQuantities.Count || productsIds.Count == 0 || customerOrder is null || clerkOrder is null)
        {
            return RedirectToPage("/Error");
        }

        //create new order
        var newOrder = new Order
        {
            Customer = customerOrder,
            Clerk = clerkOrder,
            OrderDate = DateTime.Now,
            OrderStatus = "Opened"
        };
            
        for (var i = 0; i < productsIds.Count; i++)
        {
            var product = await _context.Products.FindAsync(productsIds[i]);
            if (product == null)
            {
                //return error page if product is not found
                return RedirectToPage("/Error");
            }
                
            //create new order row
            var newOrderRow = new OrderRow
            {
                Order = newOrder,
                Product = product,
                Quantity = productsQuantities[i]
            };
            
            //save order row
            await _context.OrdersRows.AddAsync(newOrderRow);
        }
        //save order
        await _context.Orders.AddAsync(newOrder);
        
        // Save changes to the database
        await _context.SaveChangesAsync();
            
        // add order to kitchen queue
        _queue.Enqueue(new KitchenJob(newOrder.Id));
            
        //Return success page
        return Page();
    }
}