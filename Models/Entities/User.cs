using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace MyUni.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Img { get; set; }
        public int Coin { get; set; }
        public string ResetToken { get; set; } // Token for password reset
        public DateTime? ResetTokenExpiry { get; set; } // Token expiration time
    }

}
