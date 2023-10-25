using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages
{
    public class Index : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly JobQueue<KitchenJob> _queue;
        

        public Index(ApplicationDbContext context, JobQueue<KitchenJob> queue)
        {
            _context = context;
            _queue = queue;
        }
        
        // Modèle pour un nouveau client
        [BindProperty] public Customer NewCustomer { get; set; } = new()
        
        {
            PhoneNumber = "",
            Name = "",
            Email = "",
            Address = ""
        };
        public int ClerkId { get; set; }

        // Liste de clients depuis la base de données (simulée ici)
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        // Statistiques
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public Customer EditedCustomer { get; set; } = new(){
            PhoneNumber = "",
            Name = "",
            Email = "",
            Address = ""
        };
        public int? EditingCustomerId { get; set; }
    
        
        public async Task OnGetAsync(int id)
        {
            ClerkId = id;
            Customers = await _context.Customers.ToListAsync();
            TotalOrders = 10; // Exemple simulé
            TotalRevenue = 500.50m; // Exemple simulé
            
        }
        /*
        public IActionResult OnPost()
        {
            _queue.Enqueue(new KitchenJob());
            return RedirectToPage();
        }
        */
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Ajouter le nouveau client à la base de données
                _context.Customers.Add(NewCustomer);
                await _context.SaveChangesAsync();
                
                // Effacer les champs après avoir ajouté le client
                NewCustomer = new Customer
                {
                    PhoneNumber = "",
                    Name = "",
                    Email = "",
                    Address = ""
                };

                // Mettre à jour la liste des clients
                Customers = await _context.Customers.ToListAsync();
            }

            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostDeleteAsync(int? customerId)
        {
            if (customerId != null)
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
            }

            // Mettre à jour la liste des clients
            Customers = await _context.Customers.ToListAsync();

            return Page();
        }
        
        public IActionResult OnPostEdit(int? customerId)
        {
            if (customerId != null)
            {
                EditingCustomerId = customerId;
                EditedCustomer = _context.Customers.Find(customerId);
            }
            else
            {
                EditingCustomerId = null;
            }
    
            // Mettre à jour la liste des clients
            Customers = _context.Customers.ToList();
    
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync(int? EditingCustomerId, string EditedCustomerName, string EditedCustomerAddress, string EditedCustomerPhoneNumber)
        {

            if (/* ModelState.IsValid && */ EditingCustomerId != null)
            {
                var customerToEdit = await _context.Customers.FindAsync(EditingCustomerId);
                Console.WriteLine("Customer to edit:"+customerToEdit);

                if (customerToEdit != null)
                {
                    customerToEdit.Name = EditedCustomerName;
                    customerToEdit.Address = EditedCustomerAddress;
                    customerToEdit.PhoneNumber = EditedCustomerPhoneNumber;

                    await _context.SaveChangesAsync();
                }
            }

            // Réinitialiser les valeurs d'édition
            EditedCustomer = new Customer(){
                PhoneNumber = "",
                Name = "",
                Email = "",
                Address = ""
            };

            // Mettre à jour la liste des clients
            Customers = await _context.Customers.ToListAsync();

            return Page();
        }


    }

}