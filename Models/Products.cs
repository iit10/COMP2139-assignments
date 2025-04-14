


using System.ComponentModel.DataAnnotations;

namespace SmartInventory3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int LowStockThreshold { get; set; }
    }
}

