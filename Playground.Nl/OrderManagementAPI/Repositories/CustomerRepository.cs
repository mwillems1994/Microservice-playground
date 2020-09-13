using System.Linq;
using Playground.Nl.OrderManagementAPI.Nl.Context;
using Playground.Nl.OrderManagementAPI.Nl.DbModels;
using Playground.Nl.OrderManagementAPI.Nl.Extensions;
using Playground.Nl.OrderManagementAPI.Nl.Helpers;

namespace Playground.Nl.OrderManagementAPI.Nl.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(OrderManagementDbContext db, IUserPrincipalAccessor userPrincipalAccessor) : base(
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
                customers = customers.Where(u => u.CustomerId == options.Id);
            }

            return customers;
        }
    }

    public class CustomerQueryOptions
    {
        public string? Id { get; set; }
    }
}