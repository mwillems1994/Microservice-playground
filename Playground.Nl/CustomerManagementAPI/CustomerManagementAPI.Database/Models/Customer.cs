using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Playground.Nl.CustomerManagementAPI.Database.Models
{
    public class Customer : IdentityUser
    {
        public string Firstname { get; set; } = string.Empty;
        public string? Insertion { get; set; }
        public string LastName { get; set; } = string.Empty;
        [MaxLength(16)]
        public string? PostalCode { get; set; }
        [MaxLength(16)]
        public string? HouseNumber { get; set; }
        [MaxLength(128)]
        public string? Address { get; set; }
        [MaxLength(128)]
        public string? City { get; set; }
    }
}