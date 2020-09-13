using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Nl.OrderManagementAPI.Nl.DbModels
{
    [Table(nameof(Order))]
    public class Order
    {
        [Required, Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string Remarks { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;
        public Customer? Customer { get; set; }

        public ICollection<OrderItem> OrderItems = new HashSet<OrderItem>();
    }
}