using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ProfileDto
{
    public class EditProfileDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Surname { get; set; }

        public bool? Gender { get; set; }
    }
}