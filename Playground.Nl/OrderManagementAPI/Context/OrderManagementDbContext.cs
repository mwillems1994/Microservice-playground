using System;
using Microsoft.EntityFrameworkCore;
using Playground.Nl.OrderManagementAPI.Nl.DbModels;
using Polly;

namespace Playground.Nl.OrderManagementAPI.Nl.Context
{
    public class OrderManagementDbContext : DbContext
    {
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
        public void MigrateDb()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(3, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }
}