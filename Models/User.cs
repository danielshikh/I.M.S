using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "The user name must be at least 3 characters long.")]
        public string UserName { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The user password must be at least 3 characters long.")]
        public string Password { get; set; }
        public int BranchId { get; set; }
    }
}
