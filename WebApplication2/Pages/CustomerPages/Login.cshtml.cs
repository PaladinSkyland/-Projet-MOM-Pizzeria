using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Pages.CustomerPages
{
    public class CustomerLoginPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CustomerLoginPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                var customer = await _context.Customers
                    .Where(c => c.Email == Email)
                    .FirstOrDefaultAsync();

                if (customer is not null)
                {
                    // Redirect user to its page
                    return RedirectToPage("Index", new { id = customer.Id });
                }
            }
            // If email is invalid, redirect to an error page
            return RedirectToPage("/Error");
        }
    }
}