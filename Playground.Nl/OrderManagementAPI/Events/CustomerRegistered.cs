using System;
using Pitstop.Infrastructure.Messaging;

namespace Playground.Nl.OrderManagementAPI.Nl.Events
{
    public class CustomerRegistered : Event
    {
        public readonly string CustomerId;
        public readonly string Firstname;
        public readonly string Insertion;
        public readonly string Lastname;

        public CustomerRegistered(Guid messageId, string customerId, string firstname, string insertion, string lastname, string city) : base(messageId)
        {
            CustomerId = customerId;
            Firstname = firstname;
            Insertion = insertion;
            Lastname = lastname;
        }
    }
}
