using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Services.Events;
using Playground.Nl.CustomerManagementAPI.Services.Models;
using Playground.Nl.CustomerManagementAPI.Services.Models.Customer;

namespace Playground.Nl.CustomerManagementAPI.Services.Mappers
{
    public static class Mappers
    {
        public static Customer MapToCustomerModel(this CreateCustomerModel model) => new Customer
        {
            Firstname = model.FirstName,
            Insertion = model.Insertion,
            LastName = model.LastName,
            UserName = model.Email,
            Email = model.Email,
            PostalCode = model.PostalCode,
            HouseNumber = model.HouseNumber,
            Address = model.Address,
            City = model.City
        };
        
        public static CustomerRegistered MapToCustomerRegistered(this Customer command) => new CustomerRegistered
        {
            CustomerId = command.Id,
            FirstName = command.Firstname,
            Insertion = command.Insertion,
            LastName = command.LastName
        };
        
        public static async Task<CustomerModel> MapToCustomerModel(this IQueryable<Customer> command) => await command.Select(customer => customer.MapToCustomerModel()).FirstOrDefaultAsync();

        public static async Task<CustomerModel[]> MapToCustomerModels(this IQueryable<Customer> command) => await command.Select(customer => customer.MapToCustomerModel()).ToArrayAsync();
        
        public static CustomerModel MapToCustomerModel(this Customer command) => new CustomerModel
        {
            Id = command.Id,
            Firstname = command.Firstname,
            Insertion = command.Insertion,
            Lastname = command.LastName
        };
    }
}