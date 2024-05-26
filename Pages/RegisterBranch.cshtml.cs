using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventorySystem.Pages
{
    public class RegisterBranchModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;

        [BindProperty]
        public Branch Branch { get; set; }

        [BindProperty]
        public User User { get; set; }

        public RegisterBranchModel(InventoryDbContext InventoryDbContext)
        {
            this.inventoryDbContext = InventoryDbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

           if (ModelState.IsValid)
            {
                inventoryDbContext.Branch.Add(Branch);
                await inventoryDbContext.SaveChangesAsync();
                User.BranchId = Branch.Id;
                inventoryDbContext.User.Add(User);
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
