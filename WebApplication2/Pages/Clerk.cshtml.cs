using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages
{
    public class Clerk : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly JobQueue<KitchenJob> _queue;

        public Clerk(ApplicationDbContext context, JobQueue<KitchenJob> queue)
        {
            _context = context;
            _queue = queue;
        }
        
        // Modèle pour un nouveau client
        [BindProperty] public Customer NewCustomer { get; set; } = new();

        // Liste de clients depuis la base de données (simulée ici)
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        // Statistiques
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        
        public async Task OnGetAsync()
        {
            Customers = await _context.Customers.ToListAsync();
            TotalOrders = 10; // Exemple simulé
            TotalRevenue = 500.50m; // Exemple simulé
        }

        public IActionResult OnPost()
        {
            _queue.Enqueue(new KitchenJob());
            return RedirectToPage();
        }
    }

}