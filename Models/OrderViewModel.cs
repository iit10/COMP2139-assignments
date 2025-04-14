
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory3.Models
{
    public class OrderItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class OrderViewModel
    {
        [Required]
        public string GuestName { get; set; }

        [Required, EmailAddress]
        public string GuestEmail { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }
}
