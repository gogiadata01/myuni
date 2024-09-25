using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; } // The token sent in the recovery link

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; } // The new password entered by the user
    }
}
