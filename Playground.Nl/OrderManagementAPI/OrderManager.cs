using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pitstop.Infrastructure.Messaging;
using Playground.Nl.OrderManagementAPI.Nl.Context;
using Playground.Nl.OrderManagementAPI.Nl.DbModels;
using Playground.Nl.OrderManagementAPI.Nl.Events;
using Playground.Nl.OrderManagementAPI.Nl.Repositories;
using Serilog;

namespace Playground.Nl.OrderManagementAPI.Nl
{
    public class OrderManager : IHostedService, IMessageHandlerCallback
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

    
        public OrderManager(IMessageHandler messageHandler, IServiceScopeFactory serviceScopeFactory)
        {
            _messageHandler = messageHandler;
            _serviceScopeFactory = serviceScopeFactory;
        }
    
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }
    
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }
    
        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            try
            {
                var messageObject = MessageSerializer.Deserialize(message);
                switch (messageType)
                {
                    case "CustomerRegistered":
                        // TODO: try parse to object and remove '!'
                        await HandleAsync(messageObject.ToObject<CustomerRegistered>()!);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while handling {messageType} event.");
            }
    
            return true;
        }
    
        private async Task HandleAsync(CustomerRegistered cr)
        {
            Log.Information("Register customer: {Id}, {Firstname}, {Insertion}, {Lastname}", 
                cr.CustomerId, cr.Firstname, cr.Insertion, cr.Lastname);
    
            Customer customer = new Customer
            {
                CustomerId = cr.CustomerId,
                Firstname = cr.Firstname,
                Insertion = cr.Insertion,
                Lastname = cr.Lastname
            };
            using var scope = _serviceScopeFactory.CreateScope();
            var customerRepository = scope.ServiceProvider.GetRequiredService<CustomerRepository>();
            customerRepository.Add(customer);
            await customerRepository._db.SaveChangesAsync();
        }
    }
}