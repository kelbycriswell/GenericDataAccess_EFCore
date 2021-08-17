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
            modelBuilder.Entity<Customer>().HasKey(k => k.ID);
            modelBuilder.Entity<Order>().HasKey(k => k.ID);
            modelBuilder.Entity<LineItem>().HasKey(k => k.ID);
            modelBuilder.Entity<Product>().HasKey(k => k.ID);

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
