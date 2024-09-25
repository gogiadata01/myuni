using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyUni.Data;
using MyUni.Migrations;
using MyUni.Models.Entities;
using System.Text;
using System.Security.Cryptography;
using MyUni.Models;
using Microsoft.EntityFrameworkCore;
namespace MyUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            // Retrieve all users without including the password in the response
            var allUsers = dbContext.MyUser
                                    .Select(user => new
                                    {
                                        user.Id,
                                        user.Email,
                                        user.Name
                                        // Add other properties except Password
                                    })
                                    .ToList();

            if (allUsers == null || allUsers.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(allUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByid(int id)
        {
            var User = dbContext.MyUser.FirstOrDefault(card => card.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }




        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto newUserDto)
        {
            if (newUserDto == null)
            {
                return BadRequest("User data is required.");
            }

            // Optional: Validate if a user with the same email already exists
            var existingUser = dbContext.MyUser.FirstOrDefault(u => u.Email == newUserDto.Email);
            if (existingUser != null)
            {
                return Conflict("A user with the same email already exists.");
            }

            // Hash the password using SHA256
            string hashedPassword = null;
            if (!string.IsNullOrEmpty(newUserDto.Password))
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(newUserDto.Password));
                    hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
                }
            }

            // Map the DTO to the User entity
            var newUser = new User
            {
                Name = newUserDto.Name,
                Email = newUserDto.Email,
                Password = hashedPassword,
                Type = newUserDto.Type,
                Img = newUserDto.Img,
                Coin = newUserDto.Coin,
                ResetToken = newUserDto.ResetToken,

            };

            // Add the new user to the database
            dbContext.MyUser.Add(newUser);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetUserByid), new { id = newUser.Id }, newUser);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] UserSignInDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            var user = dbContext.MyUser.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Hash the provided password and compare it with the stored hashed password
            string hashedPassword;
            using (var sha256 = SHA256.Create())
            {
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
            }

            if (hashedPassword != user.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Authentication successful
            return Ok(new { Message = "SignIn successful", UserId = user.Id, userName = user.Name, email = user.Email, password = user.Password, type = user.Type, img = user.Img, coin = user.Coin,resettoken =  user.ResetToken });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Find the event card by its ID
            var user = dbContext.MyUser.FirstOrDefault(card => card.Id == id);

            // Check if the event card exists
            if (user == null)
            {
                return NotFound(new { message = "user  not found" });
            }

            // Remove the event card from the database
            dbContext.MyUser.Remove(user);
            dbContext.SaveChanges();

            return Ok(new { message = "User  deleted successfully" });
        }
        public class UpdateCoinDto
        {
            public int NewCoinValue { get; set; }
        }
        [HttpPut("{id}/coin")]
        public IActionResult UpdateCoin(int id, [FromBody] UpdateCoinDto dto)
        {
            var user = dbContext.MyUser.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.Coin = dto.NewCoinValue;
            dbContext.SaveChanges();

            return Ok(new { message = "User coin updated successfully", userId = user.Id, newCoin = user.Coin });
        }


        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null || string.IsNullOrEmpty(resetPasswordDto.Token) || string.IsNullOrEmpty(resetPasswordDto.NewPassword))
            {
                return BadRequest("Invalid password reset request.");
            }

            // Find the user by the reset token
            var user = dbContext.MyUser.FirstOrDefault(u => u.ResetToken == resetPasswordDto.Token);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            // Hash the new password
            using (var sha256 = SHA256.Create())
            {
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(resetPasswordDto.NewPassword));
                user.Password = Convert.ToBase64String(hashedPasswordBytes);
            }

            // Clear the reset token and expiry time
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            dbContext.SaveChanges();

            return Ok("Password has been reset successfully.");
        }


    }
}