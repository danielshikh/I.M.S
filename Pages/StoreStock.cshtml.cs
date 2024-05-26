using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventorySystem.Pages
{
    public class StoreStockModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;

        public StoreStockModel(InventoryDbContext InventoryDbContext)
        {
            this.inventoryDbContext = InventoryDbContext;
        }

        public List<InventorySystem.Models.Product> Products { get; set; }
        public void OnGet(string SortProperty, string sortOrder, string SearchText)
        {
            var loggedInLocation = HttpContext.Session.GetString("LoggedInLocation");
            var branchId = inventoryDbContext.Branch.SingleOrDefault(b => b.Name.Equals(HttpContext.Session.GetString("LoggedInLocation"))).Id;
            IQueryable<InventorySystem.Models.Product> productsQuery = inventoryDbContext.Product.Where(p => p.BranchId.Equals(branchId));
            switch (sortOrder)
            {
                case "name":
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                default:
                    productsQuery = productsQuery.OrderByDescending(p => p.ProductId);
                    break;

            }

            Products = productsQuery.ToList();
        }
        
        public async Task OnPostAsync()
        {
            var searchString = Request.Form["searchString"];
            Products = await inventoryDbContext.Product.Where(p => p.Location.Equals(HttpContext.Session.GetString("LoggedInUserName"))).Where(p => p.Name.ToLower().Contains(searchString) || p.Description.ToLower().Contains(searchString) || p.ProductId.Contains(searchString)).ToListAsync();
        }
    }
   
}
