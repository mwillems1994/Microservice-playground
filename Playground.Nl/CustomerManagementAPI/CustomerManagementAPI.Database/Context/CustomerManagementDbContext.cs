using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Polly;

namespace Playground.Nl.CustomerManagementAPI.Database.Context
{
    public class CustomerManagementDbContext : IdentityDbContext<Customer>
    {
        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .ToTable(nameof(Customer));
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