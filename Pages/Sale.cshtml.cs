using InventorySystem.Data; 
using InventorySystem.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient; 
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Pages
{
    public class SaleModel : PageModel
    {
        private readonly InventoryDbContext inventoryDbContext;

        public SaleModel(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }

        public List<InventorySystem.Models.Product> Products { get; set; }
        public List<InventorySystem.Models.Product> SoldItems { get; set; }
       
        public void OnGet()
        {
            IQueryable<InventorySystem.Models.Product> productsQuery = inventoryDbContext.Product.Where(p => p.Location.Equals(HttpContext.Session.GetString("LoggedInUserName")));
            Products = productsQuery.ToList();
        }

        public string CheckStock(List<InventorySystem.Models.Product> soldItems)
        {
            string message = null;
            foreach (var product in soldItems)
            {
                if (product.Quantity <= product.Threshold)
                {
                    message += product.Name +" barcode: "+ product.ProductId + " left in stock " + product.Quantity + " units. ";
                }
            }
            return message;
        }

        public void OnPostUpdateQuantities()
        {
            SoldItems = new List<InventorySystem.Models.Product>();
            var submittedQuantities = new Dictionary<int, int>(); 

            // Extract submitted quantities from form data
            foreach (var keyValuePair in Request.Form)
            {
                if (keyValuePair.Key.StartsWith("quantity["))
                {
                    var productId = int.Parse(keyValuePair.Key.Split('[')[1].Split(']')[0]);
                    var quantity = int.Parse(keyValuePair.Value);
                    submittedQuantities.Add(productId, quantity);
                }
            }


            foreach (var (productId, quantity) in submittedQuantities)
            {
                var product = inventoryDbContext.Product.FirstOrDefault(p => p.Id == productId);
                if (quantity > 0)
                {
                    SoldItems.Add(product);
                    if (product != null)
                    {
                        product.Quantity -= quantity; 
                        inventoryDbContext.SaveChanges();
                    }
                }
            }

            IQueryable<InventorySystem.Models.Product> productsQuery = inventoryDbContext.Product.Where(p => p.Location.Equals(HttpContext.Session.GetString("LoggedInUserName")));
            Products = productsQuery.ToList();

             ViewData["Message"] = "Sale was made successfully";
            if(SoldItems.Count > 0)
                TempData["StockAlert"] = CheckStock(SoldItems);
        }
        public async Task OnPostAsync()
        {
            var searchString = Request.Form["searchString"];
            Products = await inventoryDbContext.Product.Where(p => p.Location.Equals(HttpContext.Session.GetString("LoggedInUserName"))).Where(p => p.Name.ToLower().Contains(searchString) || p.Description.ToLower().Contains(searchString)).ToListAsync();
        }
    }

   
}
