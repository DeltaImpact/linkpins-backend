using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ParseDto
{
    public class ParsePageDto
    {
        [StringLength(2000, ErrorMessage = "{0} must be lower than {2} characters.", MinimumLength = 3)]
        [Display(Name = "Site url")]
        public string Img { get; set; }
        [Required] public string Url { get; set; }

        public int MinTextLenght { get; set; } = 80;
    }
}