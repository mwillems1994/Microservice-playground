using System.Linq;
using Playground.Nl.CustomerManagementAPI.Database.Context;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Services.Extensions;
using Playground.Nl.CustomerManagementAPI.Services.Helpers;

namespace Playground.Nl.CustomerManagementAPI.Services.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(CustomerManagementDBContext db, IUserPrincipalAccessor userPrincipalAccessor) : base(
            db, userPrincipalAccessor)
        {
        }

        public IQueryable<Customer> FindCustomer(string customerId)
        {
            var customer = AllCustomers(new CustomerQueryOptions
            {
                Id = customerId
            });

            return customer;
        }

        public IQueryable<Customer> AllCustomers(CustomerQueryOptions? options = null)
        {
            options ??= new CustomerQueryOptions();

            return All
                .FilterOnId(options);
        }
    }

    internal static class CustomerQueryExtensions
    {
        internal static IQueryable<Customer> FilterOnId(
            this IQueryable<Customer> customers,
            CustomerQueryOptions options)
        {
            if (options.Id.IsNotNullOrEmpty())
            {
                customers = customers.Where(u => u.Id == options.Id);
            }

            return customers;
        }
    }

    public class CustomerQueryOptions
    {
        public string? Id { get; set; }
    }
}