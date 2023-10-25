using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Coca",
                        Price = 2.5,
                        Volume = .33
                    },
                    new Drink
                    {
                        Name = "Fanta",
                        Price = 2.5,
                        Volume = .33
                    },
                    new Drink
                    {
                        Name = "Sprite",
                        Price = 2.5,
                        Volume = .33
                    },
                    new Drink
                    {
                        Name = "Ice Tea",
                        Price = 2.5,
                        Volume = .33
                    }
                );
            }
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Client 1",
                        Email = "client1@mail.com",
                        Address = "client 1 home",
                        PhoneNumber = "client 1 phone"
                    },

                    new Customer
                    {
                        Name = "Client 2",
                        Email = "client2@mail.com",
                        Address = "client 2 home",
                        PhoneNumber = "client 2 phone"
                    },

                    new Customer
                    {
                        Name = "Client 3",
                        Email = "client3@mail.com",
                        Address = "client 3 home",
                        PhoneNumber = "client 3 phone"
                    },

                    new Customer
                    {
                        Name = "Client 4",
                        Email = "client4@mail.com",
                        Address = "client 4 home",
                        PhoneNumber = "client 4 phone"
                    }
                );
            }

            if (!context.Clerks.Any())
            {
                context.Clerks.AddRange(
                    new Clerk
                    {
                        Name = "Clerk 1",
                        Email = "clerk1@mail.com",
                        NumberOrders = 0,
                        Address = "Ta ville",
                        JobTitle = "Clerk",
                        Gender = "Non binaire",
                        HireDate = "Aujourd'hui",
                        Salary = 0
                    });
            }

            context.SaveChanges();
        }
    }
}