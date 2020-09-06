using System.ComponentModel.DataAnnotations;
using Pitstop.Infrastructure.Messaging;

namespace Playground.Nl.CustomerManagementAPI.Nl.Models
{
    public class CreateCustomerInputModel : Command
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        public string FirstName { get; set; } = string.Empty;

        public string? Insertion { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail is verplicht")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Herhaal wachtwoord is verplicht")]
        [Compare(nameof(Password), ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string RepeatPassword { get; set; } = string.Empty;
        public string? PostalCode { get; set; }
        public string? HouseNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}