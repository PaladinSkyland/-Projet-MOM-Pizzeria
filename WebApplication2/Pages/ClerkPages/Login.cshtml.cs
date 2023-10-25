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

                if (clerk != null)
                {
                    // Stocker l'ID du client dans la session
                    HttpContext.Session.SetInt32("ClerkId", clerk.Id);

                    // Rediriger l'utilisateur vers la page personnalisée
                    return RedirectToPage("Index", new { id = clerk.Id });
                }
            }

            // L'e-mail n'existe pas dans la base de données ou le champ est vide, vous pouvez rediriger l'utilisateur vers une page d'erreur ou de connexion.
            // Par exemple, remplacez "Error" par la page de votre choix.
            return RedirectToPage("Error");
        }
    }
}