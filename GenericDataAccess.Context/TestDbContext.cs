using GenericDataAccess.Context.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDataAccess.Context
{
    public class TestDb : DbContext
    {
        public TestDb()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=itprog2;Initial Catalog=TestDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setup Relationships
            modelBuilder.Entity<Customer>().HasKey(k => k.ID);
            modelBuilder.Entity<Customer>().HasMany(o => o.Orders).WithOne();
            modelBuilder.Entity<Order>().HasKey(k => k.ID);
            modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne();
            modelBuilder.Entity<LineItem>().HasKey(k => new { k.OrderID, k.LineNo });
            modelBuilder.Entity<LineItem>().HasOne(h => h.Item).WithMany();
            modelBuilder.Entity<Product>().HasKey(k => k.ID);

            //Seed Data

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    ID = 1,
                    Deleted = false,
                    FName = "Test",
                    MI = "A",
                    LName = "Person",
                    DateOfBirth = new DateTimeOffset(new DateTime(2000, 05, 30)),
                    Gender = Gender.Male,
                    StreetAddress = "437 North Wasatch Drive",
                    Unit = null,
                    City = "Layton",
                    StateAbbr = "UT",
                    ZipCode = "84041",
                    PrimaryPhone = "801-336-3839",
                    SecondaryPhone = null,
                    Email = "123@test.comg",
                    ModifiedOn = DateTimeOffset.Now
                }
               );
            modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     ID = 1,
                     Name = "Test Item",
                     CostPerUnit = 5.00M,
                     OnHand = 12,
                     Description = "An Item to test with",
                     Deleted = false,
                     ModifiedOn = DateTimeOffset.Now
                 },
                 new Product
                 {
                     ID = 2,
                     Name = "Another Test Item",
                     CostPerUnit = 10.00M,
                     OnHand = 5,
                     Description = "An Item to test with",
                     Deleted = false,
                     ModifiedOn = DateTimeOffset.Now
                 });
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    ID = 1,
                    CustomerID = 1,
                    ModifiedOn = DateTimeOffset.Now
                });
            modelBuilder.Entity<LineItem>().HasData(
                new LineItem
                {
                    LineNo = 1,
                    OrderID = 1,
                    ProductID = 1,
                    Qty = 3,
                    ModifiedOn = DateTimeOffset.Now
                },
                 new LineItem
                 {
                     LineNo = 2,
                     OrderID = 1,
                     ProductID = 2,
                     Qty = 1,
                     ModifiedOn = DateTimeOffset.Now
                 });



            //modelBuilder.Entity<Person>(dbo =>
            //{
            //    dbo.HasNoKey();
            //});
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
