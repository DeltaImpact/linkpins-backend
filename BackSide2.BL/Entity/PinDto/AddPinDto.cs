using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity.PinDto
{
    public class AddPinDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }

        [StringLength(2000, ErrorMessage = "{0} must be lower than {2} characters.", MinimumLength = 3)]
        public string Img { get; set; }

        [StringLength(2000, ErrorMessage = "{0} must be lower than {2} characters.", MinimumLength = 3)]
        public string Link { get; set; }

        [StringLength(2000, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public long BoardId { get; set; }
    }
}