using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity
{
    public class LoginDto
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}
