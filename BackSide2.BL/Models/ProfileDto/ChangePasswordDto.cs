using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ProfileDto
{
    public class ChangePasswordDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string NewPassword { get; set; }
    }
}