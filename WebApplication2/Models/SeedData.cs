using Microsoft.EntityFrameworkCore;
using WebApplication2.DB;

namespace WebApplication2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!context.Pizzas.Any())
            {
                context.Pizzas.AddRange(
                    new Pizza
                    {
                        Name = "Mamma Mia",
                        Price = 10,
                        Size = "Normal",
                        Description = "Tomato and cheese sauce"
                    },
                    new Pizza
                    {
                        Name = "Pepperoni",
                        Price = 12,
                        Size = "Normal",
                        Description = "Tomato sauce, pepperoni and mozzarella"
                    },
                    new Pizza
                    {
                        Name = "Quattro formaggi",
                        Price = 14,
                        Size = "XL",
                        Description = "Tomato sauce and 4 italian cheese"
                    },
                    new Pizza
                    {
                        Name = "margherita",
                        Price = 11,
                        Size = "Normal",
                        Description = "Tomato sauce and mozzarella"
                    }
                );
            }
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Coca",
                        Price = 3,
                        Volume = .33
                    },
                    new Drink
                    {
                        Name = "Water",
                        Price = 1.5,
                        Volume = .5
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
                        HireDate = new DateOnly(2023, 10, 23),
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
                        HireDate = new DateOnly(2023, 10, 25),
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
                        OrderDate = DateTime.Now,
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