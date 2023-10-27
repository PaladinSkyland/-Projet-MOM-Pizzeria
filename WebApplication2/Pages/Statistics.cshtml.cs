using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages
{
    public class Stat : PageModel
    {
        private readonly ApplicationDbContext _context;


        public Stat(ApplicationDbContext context)
        {
            _context = context;

        }
        
        public double AvgOrders { get; set; }
        public double AvgAccountsRecivable { get; set; }
        
        
        public class ClerkOrderCount
        {
            public string ClerkName { get; set; }
            public int OrderCount { get; set; }
        }

        public List<ClerkOrderCount> ClerkOrderCounts { get; set; }
        
        public class DelOrderCount
        {
            public string DelName { get; set; }
            public int OrderCount { get; set; }
        }

        public List<DelOrderCount> DelOrderCounts { get; set; }

        public List<Order> OrderByDateToday { get; set; }
        public List<Order> OrderByDateYesterday { get; set; }
        public List<Order> OrderByDate2plus { get; set; }
        
        
        public async Task OnGetAsync()
        {

            var orders = _context.Orders.Include(o => o.OrdersRows); 
            double totalPrice = orders.Sum(order => order.OrdersRows.Sum(orderRow => orderRow.Quantity * orderRow.Product.Price));

            
            var orderqte = _context.Orders.Count();
            AvgOrders = totalPrice/orderqte;

            var cutqte = _context.Customers.Count();
            
            AvgAccountsRecivable = totalPrice/cutqte; 
            
            ClerkOrderCounts = _context.Clerks
                .Select(clerk => new ClerkOrderCount
                {
                    ClerkName = clerk.Name,
                    OrderCount = clerk.Orders.Count()
                })
                .ToList();
            
            DelOrderCounts = _context.Deliverers
                .Select(del => new DelOrderCount
                {
                    DelName = del.Name,
                    OrderCount = del.Orders.Count()
                })
                .ToList();

            OrderByDateToday = _context.Orders
                .Include(o=> o.Customer)
                .Include(o=> o.Clerk)
                .Include(o=> o.Deliverer)
                .Where(o => o.OrderDate == "today")
                .ToList();
            
            OrderByDateYesterday = _context.Orders
                .Include(o=> o.Customer)
                .Include(o=> o.Clerk)
                .Include(o=> o.Deliverer)
                .Where(o => o.OrderDate == "yesterday")
                .ToList();
            
            OrderByDate2plus = _context.Orders
                .Include(o=> o.Customer)
                .Include(o=> o.Clerk)
                .Include(o=> o.Deliverer)
                .Where(o => o.OrderDate == "+2 days")
                .ToList();



        }
        
        
        
    }
}