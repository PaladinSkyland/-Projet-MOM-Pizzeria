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

                if (deliverer != null)
                {
                    // Stocker l'ID du client dans la session
                    HttpContext.Session.SetInt32("DelivererId", deliverer.Id);

                    // Rediriger l'utilisateur vers la page personnalisée
                    return RedirectToPage("Index", new { id = deliverer.Id });
                }
            }

            // L'e-mail n'existe pas dans la base de données ou le champ est vide, vous pouvez rediriger l'utilisateur vers une page d'erreur ou de connexion.
            // Par exemple, remplacez "Error" par la page de votre choix.
            return RedirectToPage("Error");
        }
    }
}