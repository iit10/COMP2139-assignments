
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory3.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
