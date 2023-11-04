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
        public List<Clerk> ClerkOrders { get; set; } = new();
        public List<Deliverer> DelivererOrders { get; set; } = new();
        public List<Order> OrdersByDateToday { get; set; } = new();
        public List<Order> OrdersByDateYesterday { get; set; } = new();
        public List<Order> OrdersByDate2Days { get; set; } = new();
        
        public async Task OnGetAsync()
        {
            var totalPrice = (await _context.Orders
                .Include("OrdersRows.Product")
                .ToListAsync())
                .Sum(order => order.Price);
            
            var nbOrders = await _context.Orders.CountAsync();
            AvgOrders = totalPrice / nbOrders;

            var nbCustomers = await _context.Customers.CountAsync();
            AvgAccountsReceivable = totalPrice / nbCustomers; 
            
            ClerkOrders = await _context.Clerks
                .Include("Orders.OrdersRows.Product")
                .ToListAsync();
            
            DelivererOrders = await _context.Deliverers
                .Include("Orders.OrdersRows.Product")
                .ToListAsync();

            OrdersByDateToday = await _context.Orders
                .Include("Customer")
                .Include("Clerk")
                .Include("Deliverer")
                .Where(o => o.OrderDate.Date == DateTime.Today)
                .ToListAsync();
            
            OrdersByDateYesterday = await _context.Orders
                .Include("Customer")
                .Include("Clerk")
                .Include("Deliverer")
                .Where(o => o.OrderDate.Date == DateTime.Today.AddDays(-1))
                .ToListAsync();
            
            OrdersByDate2Days = await _context.Orders
                .Include("Customer")
                .Include("Clerk")
                .Include("Deliverer")
                .Where(o => o.OrderDate.Date == DateTime.Today.AddDays(-2))
                .ToListAsync();
        }
    }
}