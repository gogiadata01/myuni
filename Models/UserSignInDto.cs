using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class UserSignInDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
