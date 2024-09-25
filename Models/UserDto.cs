using System.ComponentModel.DataAnnotations;

namespace MyUni.Models
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Img { get; set; }

        public int Coin { get; set; }

        public string ResetToken { get; set; } // This should be in your model
    }
}
