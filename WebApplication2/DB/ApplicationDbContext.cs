using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DB
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Clerk> Clerks { get; set; } = null!;
        public DbSet<Deliverer> Deliverers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Drink> Drinks { get; set; } = null!;
        public DbSet<Pizza> Pizzas { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderRow> OrdersRows { get; set; } = null!;
    }
}