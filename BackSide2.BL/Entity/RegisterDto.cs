using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 3)]

        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 3)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}