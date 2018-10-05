using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ParseDto
{
    public class ParsePageDto
    {
        [Url] [Required] public string Url { get; set; }

        public int MinTextLenght { get; set; } = 80;
        public int MaxTextLenght { get; set; } = 500;
    }
}