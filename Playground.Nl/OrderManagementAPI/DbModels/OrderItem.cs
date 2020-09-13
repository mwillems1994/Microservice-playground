using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Nl.OrderManagementAPI.Nl.DbModels
{
    [Table(nameof(OrderItem))]
    public class OrderItem
    {
        [Required, Key]
        public string OrderItemId { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public Order? Order { get; set; }
    }
}