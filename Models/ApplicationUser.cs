using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
