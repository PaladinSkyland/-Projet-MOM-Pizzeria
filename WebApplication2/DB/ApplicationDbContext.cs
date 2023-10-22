using Microsoft.EntityFrameworkCore;

namespace VotreProjet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        // public DbSet<>
        // Déclarez d'autres DbSet pour chaque table de votre base de données
        
    }
}