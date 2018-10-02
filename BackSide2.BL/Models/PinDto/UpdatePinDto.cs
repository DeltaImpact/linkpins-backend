using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity.PinDto
{
    public class UpdatePinDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
        //[StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }
    }
}
