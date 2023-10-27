using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.DelivererPages
{
    public class DelivererPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DelivererPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Deliverer Deliverer { get; set; } = new()
        {
            Vehicle = "",
            Name = "",
            Email = "",
            Address = "",
            JobTitle = "",
            Gender = "",
            HireDate = default,
            Salary = 0
        };

        public IList<string> Notifications { get; set; } = new List<string>();
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var deliverer = await _context.Deliverers.FindAsync(id);

            if (deliverer == null)
            {
                return NotFound();
            }

            Deliverer = deliverer;
            Notifications = RabbitClient.GetNotifications("deliverers_exchange", $"deliverer{id}");

            return Page();
        }
    }
}