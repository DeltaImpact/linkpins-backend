using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.PinDto
{
    public class AddPinDto
    {
        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} should be shorter than {2} symbols.")]
        public string Description { get; set; }

        [Url] public string Img { get; set; }
        [Url] public string Link { get; set; }
        public long BoardId { get; set; }
    }
}