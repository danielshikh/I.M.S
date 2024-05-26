using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace InventorySystem.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }
   
        public DbSet<InventorySystem.Models.User> User  { get; set; }
        public DbSet<InventorySystem.Models.Product> Product { get; set; }
        public DbSet<InventorySystem.Models.Branch> Branch { get; set; }
        //public DbSet<InventorySystem.Models.CartItem> ShoppingCartItems { get; set; }


    }
}
