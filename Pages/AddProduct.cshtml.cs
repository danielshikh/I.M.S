using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace InventorySystem.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;
        [BindProperty]
        public Product Product { get; set; }
        public List<InventorySystem.Models.Branch> Branches { get; set; }

        public AddProductModel(InventoryDbContext InventoryDbContext)
        {
            this.inventoryDbContext = InventoryDbContext;
        }
        public void OnGet()
        {
            if(HttpContext.Session.GetString("LoggedInUserName").Equals("Admin"))
            {
                Branches = inventoryDbContext.Branch
                .Where(branch => branch.Name != "Admin")
                .ToList();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!HttpContext.Session.GetString("LoggedInUserName").Equals("Admin"))
            {
                Product.Location = HttpContext.Session.GetString("LoggedInLocation");
                Product.BranchId = inventoryDbContext.User.SingleOrDefault(u => u.UserName.Equals(HttpContext.Session.GetString("LoggedInUserName"))).BranchId;
            }

            else
            {
                Product.Location = inventoryDbContext.Branch.Find(Product.BranchId).Name;
            }
            ModelState.Remove("Product.Location");

            if (ModelState.IsValid)
            {
                inventoryDbContext.Product.Add(Product);
                await inventoryDbContext.SaveChangesAsync();
                return RedirectToPage("/MainPage");
            }
            else
            {
                return Page();
            }
        }
    }
}
