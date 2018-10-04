using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.BoardPinDto
{
    public class AddPinToBoardDto
    {
        [Required]
        public long PinId { get; set; }

        [Required]
        public long BoardId { get; set; }
    }
}
