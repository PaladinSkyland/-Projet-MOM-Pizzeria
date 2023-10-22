using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;

namespace WebApplication2.Pages
{
    public class Clerk : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Clerk(ApplicationDbContext context)
        {
            _context = context;
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
        }

        public IActionResult OnPost()
        {
            // Ajouter un nouveau client à la liste (simulé)
            Customers.Add(NewCustomer);

            // Réinitialiser le formulaire
            NewCustomer = new Customer();

            return Page();
        }
    }

}