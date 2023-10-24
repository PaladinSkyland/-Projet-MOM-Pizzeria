using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public Deliverer? Deliverer { get; set; }
        public IList<string> Notifications { get; set; } = new List<string>();
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deliverer = await _context.Deliverers.FirstOrDefaultAsync(m => m.Id == id);

            if (Deliverer == null)
            {
                return NotFound();
            }

            Notifications = RabbitClient.GetNotifications("deliverers_exchange", $"deliverer{id}");

            return Page();
        }
    }
}