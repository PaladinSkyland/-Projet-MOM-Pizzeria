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
                context.Clerks.Add(
                    new Clerk
                    {
                        Name = "clerk 1",
                        Email = "clerk1@mail.com",
                        Address = "clerk 1 home",
                        JobTitle = "Clerk",
                        Gender = "M",
                        HireDate = "yesterday",
                        Salary = 1
                    }
                );
            }
            if (!context.Deliverers.Any())
            {
                context.Deliverers.Add(
                    new Deliverer
                    {
                        Name = "deliverer 1",
                        Email = "deliverer1@mail.com",
                        Address = "deliverer 1 home",
                        JobTitle = "Deliverer",
                        Gender = "F",
                        HireDate = "tomorrow",
                        Salary = 2,
                        Vehicle = "Car"
                    }
                );
            }
            context.SaveChanges();
            
            if (!context.Orders.Any())
            {
                context.Orders.Add(
                    new Order
                    {
                        Customer = context.Customers.First(),
                        Clerk = context.Clerks.First(),
                        OrderDate = "today",
                        OrderStatus = "Opened"
                    }
                );
            }
            context.SaveChanges();
            
            if (!context.OrdersRows.Any())
            {
                context.OrdersRows.Add(
                    new OrderRow
                    {
                        Order = context.Orders.First(),
                        Product = context.Drinks.First(),
                        Quantity = 2
                    }
                );
            }
            context.SaveChanges();
        }
    }
}