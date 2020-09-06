using System;
using Pitstop.Infrastructure.Messaging;

namespace Playground.Nl.CustomerManagementAPI.Services.Events
{
    public class CustomerRegistered : Event
    {
        public string CustomerId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? Insertion { get; set; }
        public string LastName { get; set; } = string.Empty;
    }
}