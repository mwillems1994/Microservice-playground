using Pitstop.Infrastructure.Messaging;

namespace Playground.Nl.CustomerManagementAPI.Services.Models.Customer
{
    public class CreateCustomerModel : Command
    {
        public string FirstName { get; set; } = string.Empty;
        public string? Insertion { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public string? HouseNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}