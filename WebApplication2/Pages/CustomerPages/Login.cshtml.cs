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

                if (customer != null)
                {
                    // Stocker l'ID du client dans la session
                    HttpContext.Session.SetInt32("CustomerId", customer.Id);

                    // Rediriger l'utilisateur vers la page personnalisée
                    return RedirectToPage("Index", new { id = customer.Id });
                }
            }

            // L'e-mail n'existe pas dans la base de données ou le champ est vide, vous pouvez rediriger l'utilisateur vers une page d'erreur ou de connexion.
            // Par exemple, remplacez "Error" par la page de votre choix.
            return RedirectToPage("Error");
        }
    }
}