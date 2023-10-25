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
        
        public double TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }


        public async Task OnGetAsync()
        {
            //Customers = await _context.Customers.ToListAsync();
            TotalOrders = await _context.Orders.CountAsync(); // Exemple simulé
          
            TotalRevenue = 500.50m; // Exemple simulé
        }
        
    }
}