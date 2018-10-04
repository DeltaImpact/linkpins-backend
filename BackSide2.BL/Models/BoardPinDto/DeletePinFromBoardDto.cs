using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.BoardPinDto
{
    public class DeletePinFromBoardDto
    {
        [Required] public long PinId { get; set; }

        [Required] public long BoardId { get; set; }
    }
}