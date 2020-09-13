using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
using Playground.Nl.CustomerManagementAPI.Database.Context;
using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Services.Exceptions;
using Playground.Nl.CustomerManagementAPI.Services.Helpers;
using Playground.Nl.CustomerManagementAPI.Services.Mappers;
using Playground.Nl.CustomerManagementAPI.Services.Models;
using Playground.Nl.CustomerManagementAPI.Services.Models.Customer;


namespace Playground.Nl.CustomerManagementAPI.Services.Services
{
    public class CustomerService : BaseService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IMessagePublisher _messagePublisher;

        public CustomerService(
            CustomerManagementDbContext db,
            IUserPrincipalAccessor userPrincipalAccessor,
            UserManager<Customer> userManager,
            IMessagePublisher messagePublisher
        ) : base(db, userPrincipalAccessor)
        {
            _userManager = userManager;
            _messagePublisher = messagePublisher;
        }

        public async Task<Customer> CreateCustomerAsync(CreateCustomerModel model)
        {
            var customer = model.MapToCustomerModel();

            var result = await _userManager.CreateAsync(customer, model.Password);
            
            if (!result.Succeeded)
            {
                throw new CustomException(result.Errors.Select(error => new CustomExceptionMessage
                {
                    Message = error.Description
                }).ToArray());
            }

            var e = customer.MapToCustomerRegistered();
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return customer;
        }
    }
}