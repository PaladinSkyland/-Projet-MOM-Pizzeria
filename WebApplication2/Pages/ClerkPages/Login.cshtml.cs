using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Pages.ClerkPages
{
    public class ClerkLoginPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ClerkLoginPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string? Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                var clerk = await _context.Clerks
                    .Where(c => c.Email == Email)
                    .FirstOrDefaultAsync();

                if (clerk is not null)
                {
                    // Redirect user to its page
                    return RedirectToPage("Index", new { id = clerk.Id });
                }
            }
            // If email is invalid, redirect to an error page
            return RedirectToPage("/Error");
        }
    }
}