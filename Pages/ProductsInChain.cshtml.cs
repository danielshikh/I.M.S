using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Pages
{
    public class ProductsInChainModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;

        public class BranchProductViewModel
        {
            public Branch Branch { get; set; }
            public List<Product> Products { get; set; }
        }
        public ProductsInChainModel(InventoryDbContext InventoryDbContext)
        {
            this.inventoryDbContext = InventoryDbContext;
        }
        public List<BranchProductViewModel> BranchProducts { get; set; }

        public List<InventorySystem.Models.Product> Products { get; set; }
        public void OnGet(string sortOrder)
        {
            var branches = inventoryDbContext.Branch
                .Where(branch => branch.Name != "Admin")
                .ToList();
            BranchProducts = new List<BranchProductViewModel>();

            foreach (var branch in branches)
            {
                IQueryable<Product> productsQuery = inventoryDbContext.Product
                    .Where(p => p.BranchId == branch.Id);

                switch (sortOrder)
                {
                    case "name":
                        productsQuery = productsQuery.OrderBy(p => p.Name);
                        break;
                    default:
                        productsQuery = productsQuery.OrderByDescending(p => p.ProductId);
                        break;
                }

                var branchProductViewModel = new BranchProductViewModel
                {
                    Branch = branch,
                    Products = productsQuery.ToList()
                };

                BranchProducts.Add(branchProductViewModel);
            }
        }
     
    }
}
