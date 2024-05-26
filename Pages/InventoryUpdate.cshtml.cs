using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventorySystem.Pages
{
    public class InventoryUpdateModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;
        [BindProperty]
        public Product Product { get; set; }
        public List<InventorySystem.Models.Product> Products { get; set; }  
        public InventoryUpdateModel(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }
        public String productAddress;

        public void OnGet(string sortOrder)
        {
            var loggedInUserName = HttpContext.Session.GetString("LoggedInUserName");
            var loggedInLocation = HttpContext.Session.GetString("LoggedInLocation");
            IQueryable<InventorySystem.Models.Product> productsQuery;

            if (loggedInUserName.Equals("Admin"))
            {
                productsQuery = inventoryDbContext.Product;
            }
            else
            {
                var branchId = inventoryDbContext.Branch.SingleOrDefault(b => b.Name.Equals(HttpContext.Session.GetString("LoggedInLocation"))).Id;
                productsQuery = inventoryDbContext.Product.Where(p => p.BranchId.Equals(branchId));
            }
                switch(sortOrder)
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
    }
}
