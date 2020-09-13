using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Nl.OrderManagementAPI.Nl.DbModels
{
    [Table(nameof(Product))]
    public class Product
    {
        [Required, Key]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
    }
}