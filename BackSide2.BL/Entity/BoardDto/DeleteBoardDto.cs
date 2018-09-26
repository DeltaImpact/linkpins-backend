using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Entity.BoardDto
{
    public class DeleteBoardDto
    {
        //[Required]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Board name length name must be between 3 an 256")]
        //public string Name { get; set; }


        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Board name length name must be between 3 an 256")]
        [Required]
        public long Id { get; set; }
    }
}
