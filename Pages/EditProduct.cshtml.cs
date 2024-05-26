using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;

namespace InventorySystem.Pages
{
    public class EditProductModel : PageModel
    {
        [BindProperty]
        public Product EditProduct { get; set; }
        private readonly InventoryDbContext inventoryDbContext;

        public EditProductModel(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }
        public void OnGet(int Id)
        {
            var product = inventoryDbContext.Product.Find(Id);

            if (product != null)
            {
                EditProduct = new Product()
                {
                    Id = product.Id,
                    BranchId = product.BranchId,
                    Name = product.Name,
                    Description = product.Description,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    PriceCustomer = product.PriceCustomer,
                    PriceImporter = product.PriceImporter,
                    Location = product.Location,
                    Threshold = product.Threshold,
                };
            }
        }
        public void OnPostUpdate(int Id)
        {
            if (EditProduct != null)
            {
                var existingProduct = inventoryDbContext.Product.Find(Id);
                if (existingProduct != null)
                {
                    existingProduct.Description = EditProduct.Description;
                    existingProduct.Quantity = EditProduct.Quantity;
                    existingProduct.PriceCustomer = EditProduct.PriceCustomer;
                    existingProduct.Threshold = EditProduct.Threshold;
                    inventoryDbContext.SaveChanges();
                    ViewData["Message"] = "Product Updated";
                }
            }
        }
        public IActionResult OnPostDelete(int Id)
        {
            var existingProduct = inventoryDbContext.Product.Find(Id);
            if (existingProduct != null)
            {
                inventoryDbContext.Product.Remove(existingProduct);
                inventoryDbContext.SaveChanges();
                return RedirectToPage("/InventoryUpdate");
            }
            return Page();

        }

    }
}
