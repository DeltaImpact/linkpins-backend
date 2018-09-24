using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity
{
    public class LoginDto
    {
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Required] public string Email { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        [Required] public string Password { get; set; }
    }
}
