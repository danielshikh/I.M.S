using InventorySystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Pages
{
    public class SearchModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;

        public SearchModel(InventoryDbContext InventoryDbContext)
        {
            this.inventoryDbContext = InventoryDbContext;
        }

        public List<InventorySystem.Models.Product> Products { get; set; }
        public void OnGet()
        {
            Products = new List<InventorySystem.Models.Product>();
        }

        public async Task OnPostAsync()
        {
            var searchString = Request.Form["searchString"];
            Products = await inventoryDbContext.Product.Where(p => p.Name.ToLower().Contains(searchString) || p.Description.ToLower().Contains(searchString) || p.ProductId.Contains(searchString)).ToListAsync();
        }
    }
}
