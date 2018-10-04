using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.PinDto
{
    public class DeletePinDto
    {
        [Required]
        public long Id { get; set; }
    }
}
