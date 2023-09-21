using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
