using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity.BoardDto
{
    public class AddBoardDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Description { get; set; }
        [StringLength(2000, ErrorMessage = "{0} must be lower than {2} characters.")]
        //[Display(Name = "Image url")]
        public string Img { get; set; }
        [Required] public bool IsPrivate { get; set; }
    }
}