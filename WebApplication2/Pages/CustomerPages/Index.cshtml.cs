using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.CustomerPages
{
    public class CustomerPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomerPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer? Customer { get; set; }
        public IList<string> Notifications { get; set; } = new List<string>();
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            
            Notifications = RabbitClient.GetNotifications("customers_exchange", $"customer{id}");

            return Page();
        }
    }
}