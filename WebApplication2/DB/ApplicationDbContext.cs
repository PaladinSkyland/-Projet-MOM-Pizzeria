using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        // public DbSet<>
        // Déclarez d'autres DbSet pour chaque table de votre base de données
        
    }
}