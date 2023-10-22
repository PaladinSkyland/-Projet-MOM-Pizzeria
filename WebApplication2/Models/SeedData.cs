using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApplication2.DB;

namespace WebApplication2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Drinks.Any())
                {
                    context.Drinks.AddRange(
                        new Drink
                        {
                            Name = "Coca",
                            Price = 2.5,
                            Volume = "33cl"
                        },
                        new Drink
                        {
                            Name = "Fanta",
                            Price = 2.5,
                            Volume = "33cl"
                        },
                        new Drink
                        {
                            Name = "Sprite",
                            Price = 2.5,
                            Volume = "33cl"
                        },
                        new Drink
                        {
                            Name = "Ice Tea",
                            Price = 2.5,
                            Volume = "33cl"
                        }
                    );
                }
                // Look for any movies.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

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
                context.SaveChanges();
            }
        }
    }
}