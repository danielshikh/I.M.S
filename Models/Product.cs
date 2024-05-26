using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BranchId { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "The product name must be at least 3 characters long.")]

        public string Name { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The description name must be at least 3 characters long.")]
        public string Description { get; set; }
        [StringLength(10, MinimumLength = 7, ErrorMessage = "The barcode must be at least 7 and at most 10 digits.")]
        public string ProductId { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "The quantity must be a valid integer.")]
        public int Quantity { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The price must be a valid floating-point number with up to two decimal places.")]
        public float PriceCustomer { get; set; }
        public float PriceImporter { get; set;}
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The product location must be at least 3 characters long.")]
        public string Location { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "The threshold must be a valid integer.")]

        public int Threshold { get; set; }


    }
}
