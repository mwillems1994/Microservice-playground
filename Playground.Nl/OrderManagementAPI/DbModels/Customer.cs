using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Nl.OrderManagementAPI.Nl.DbModels
{
    [Table(nameof(Customer))]
    public class Customer
    {
        [Required, Key]
        public string CustomerId { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Insertion { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; }  = new HashSet<Order>();
    }
}