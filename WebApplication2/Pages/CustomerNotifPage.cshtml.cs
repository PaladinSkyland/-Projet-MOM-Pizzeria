using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.DB;
using WebApplication2.Models;

namespace WebApplication2.Pages
{
    public class CustomerNotifPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomerNotifPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

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

            return Page();
        }
    }
}