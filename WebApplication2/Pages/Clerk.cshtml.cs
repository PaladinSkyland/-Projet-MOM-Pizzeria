using System;
using System.Collections.Generic;
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
        private readonly JobQueue<BackendService> _queue;
        private readonly ILogger<BackendService> _logger;

        public Clerk(ApplicationDbContext context, JobQueue<BackendService> queue, ILogger<BackendService> logger)
        {
            _context = context;
            _queue = queue;
            _logger = logger;
        }
        
        // Modèle pour un nouveau client
        [BindProperty]
        public Customer NewCustomer { get; set; }

        // Liste de clients depuis la base de données (simulée ici)
        public IList<Customer> Customers { get; set; }

        // Statistiques
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        
        public async Task OnGetAsync()
        {
            Customers = await _context.Customers.ToListAsync();
            TotalOrders = 10; // Exemple simulé
            TotalRevenue = 500.50m; // Exemple simulé
            _queue.Enqueue(new BackendService(_logger, _queue));
        }

        public IActionResult OnPost()
        {
            // Ajouter un nouveau client à la liste (simulé)


            // Réinitialiser le formulaire
            NewCustomer = new Customer();

            return Page();
        }
    }

}