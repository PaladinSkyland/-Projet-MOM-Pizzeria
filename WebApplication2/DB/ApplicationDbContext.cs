using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Clerk> Clerks { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        // Déclarez d'autres DbSet pour chaque table de votre base de données
        
    }
}