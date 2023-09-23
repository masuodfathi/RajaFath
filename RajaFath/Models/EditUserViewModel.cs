using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class EditUserViewModel
    {
        public string? UserId { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public string? Role { get; set; } // To hold the selected role
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Role")]
        public string? SelectedRole { get; set; }
        public List<string>? AllRoles { get; set; }
        public IEnumerable<SelectListItem>? SelectListAllRoles { get; set; }
        
    }
}
