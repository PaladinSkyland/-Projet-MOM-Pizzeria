using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;

namespace WebApplication2.Pages
{
    public class StatsPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StatsPageModel(ApplicationDbContext context)
        {
            _context = context;

        }
        
        public double AvgOrders { get; set; }
        public double AvgAccountsReceivable { get; set; }
        
        
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
            
            AvgAccountsReceivable = totalPrice/cutqte; 
            
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
                .Where(o => o.OrderDate.Date == DateTime.Today)
                .ToList();
            
            OrderByDateYesterday = _context.Orders
                .Include(o=> o.Customer)
                .Include(o=> o.Clerk)
                .Include(o=> o.Deliverer)
                .Where(o => o.OrderDate.Date == DateTime.Today.AddDays(-1))
                .ToList();
            
            OrderByDate2plus = _context.Orders
                .Include(o=> o.Customer)
                .Include(o=> o.Clerk)
                .Include(o=> o.Deliverer)
                .Where(o => o.OrderDate.Date == DateTime.Today.AddDays(-2))
                .ToList();
        }
    }
}