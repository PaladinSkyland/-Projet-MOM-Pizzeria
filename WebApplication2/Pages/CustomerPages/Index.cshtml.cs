using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public Customer Customer { get; set; } = new()
        {
            PhoneNumber = "",
            Name = "",
            Email = "",
            Address = ""
        };

        public IList<string> Notifications { get; set; } = new List<string>();
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            Customer = customer;
            Notifications = RabbitClient.GetNotifications("customers_exchange", $"customer{id}");

            return Page();
        }
    }
}