using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.ParseDto
{
    public class ParsePageDto
    {
        [Url] [Required] public string Url { get; set; }

        public int MinTextLength { get; set; } = 80;
        public int MaxTextLength { get; set; } = 500;
    }
}