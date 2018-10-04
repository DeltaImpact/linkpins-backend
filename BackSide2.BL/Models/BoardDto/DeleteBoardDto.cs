using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.BoardDto
{
    public class DeleteBoardDto
    {
        [Required]
        public long Id { get; set; }
    }
}
