using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.ClerkPages
{
    public class ClerkPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public ClerkPageModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        // Clients list from the database
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public int? EditingCustomerId { get; set; }
    
        public Clerk Clerk { get; set; } = new()
        {
            Name = "",
            Email = "",
            Address = "",
            JobTitle = "",
            Gender = "",
            HireDate = default,
            Salary = 0
        };

        public IList<string> Notifications { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customers = await _context.Customers.ToListAsync();
            var clerk = await _context.Clerks.FindAsync(id);

            if (clerk is null)
            {
                return NotFound();
            }

            Clerk = clerk;
            Notifications = RabbitClient.GetNotifications("customers_exchange", $"customer{id}");

            return Page();
        }

        public async Task<IActionResult> OnPostAddCustomerAsync(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                // Add the new client to the database
                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostDeleteCustomerAsync(int? customerId)
        {
            if (customerId is not null)
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer is not null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostEditCustomerAsync(int? customerId)
        {
            if (customerId is not null)
            {
                EditingCustomerId = customerId;
                var editedCustomer = await _context.Customers.FindAsync(customerId);
                if (editedCustomer is not null)
                {
                    // Reload clients list
                    Customers = _context.Customers.ToList();
                    return Page(); 
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveCustomerAsync(int? editingCustomerId, Customer editedCustomer)
        {
            if (ModelState.IsValid && editingCustomerId is not null)
            {
                var customerToEdit = await _context.Customers.FindAsync(editingCustomerId);
                if (customerToEdit is not null)
                {
                    customerToEdit.Name = editedCustomer.Name;
                    customerToEdit.Address = editedCustomer.Address;
                    customerToEdit.PhoneNumber = editedCustomer.PhoneNumber;

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage();
        }
    }

}