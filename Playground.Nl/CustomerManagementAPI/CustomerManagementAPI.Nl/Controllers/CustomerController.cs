using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Nl.Mappers;
using Playground.Nl.CustomerManagementAPI.Nl.Models;
using Playground.Nl.CustomerManagementAPI.Services.Extensions;
using Playground.Nl.CustomerManagementAPI.Services.Mappers;
using Playground.Nl.CustomerManagementAPI.Services.Models.Customer;
using Playground.Nl.CustomerManagementAPI.Services.Repositories;
using Playground.Nl.CustomerManagementAPI.Services.Services;

namespace Playground.Nl.CustomerManagementAPI.Nl.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController
    {
        private readonly CustomerService _customerService;
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerService customerService, CustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }
        
        [HttpGet("{customerId}")]
        public async Task<CustomerModel> CustomerAsync(string customerId)
        {
            // TODO: Add authorization checks
            // TODO: Throw proper exception when customer(id) is NullOrEmpty or not found
            var customerQuery = customerId.IsNotNullOrEmpty() 
                ? _customerRepository.FindCustomer(customerId)
                : Enumerable.Empty<Customer>().AsQueryable();

            var models = await customerQuery.MapToCustomerModel();

            return models;
        }

        [HttpGet]
        public async Task<CustomerModel[]> CustomersAsync()
        {
            // TODO: Add authorization checks
            var customersQuery = _customerRepository.AllCustomers();

            var models = await customersQuery.MapToCustomerModels();

            return models;
        }

        [HttpPost]
        public async Task CreateCustomerAsync(CreateCustomerInputModel model)
        {
            await _customerService.CreateCustomerAsync(model.MapToCreateCustomerModel());
        }
    }
}