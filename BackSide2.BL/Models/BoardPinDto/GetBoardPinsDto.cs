using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.BoardPinDto
{
    public class GetBoardPinsDto
    {
        [Required]
        public long BoardId { get; set; }
        public int Offset { get; set; } = 15;
        public int Take { get; set; } = 15;
    }
}
