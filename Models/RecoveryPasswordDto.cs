using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class RecoveryPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
