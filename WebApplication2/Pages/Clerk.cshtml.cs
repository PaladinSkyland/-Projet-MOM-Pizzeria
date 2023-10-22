using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class Clerk : PageModel
    {
        // Modèle pour un nouveau client
        [BindProperty]
        public Customer NewCustomer { get; set; }

        // Liste de clients depuis la base de données (simulée ici)
        public List<Customer> Customers { get; set; }

        // Statistiques
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        public Clerk()
        {
            // Initialisation ou récupération des données (exemple simulé)
            Customers = new List<Customer>
            {
                new Customer { Name = "Client1", Address = "Adresse1", PhoneNumber = "1234567890" },
                new Customer { Name = "Client2", Address = "Adresse2", PhoneNumber = "9876543210" }
            };

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

    // Modèle pour un client (simulé)
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}