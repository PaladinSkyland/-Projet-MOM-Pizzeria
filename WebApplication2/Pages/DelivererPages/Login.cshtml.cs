using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Pages.DelivererPages
{
    public class DelivererLoginPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DelivererLoginPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                var deliverer = await _context.Deliverers
                    .Where(d => d.Email == Email)
                    .FirstOrDefaultAsync();

                if (deliverer is not null)
                {
                    // Redirect user to its page
                    return RedirectToPage("Index", new { id = deliverer.Id });
                }
            }
            // If email is invalid, redirect to an error page
            return RedirectToPage("/Error");
        }
    }
}