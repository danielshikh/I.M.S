using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The branch name must be at least 3 characters long.")]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The branch address must be at least 2 characters long.")]
        public string Address { get; set; }
        [StringLength(10, MinimumLength = 9, ErrorMessage = "The branch phone number must be at least 9 digits long and maximum 10 digits.")]
        public string PhoneNumber { get; set; }
        
    }
}
