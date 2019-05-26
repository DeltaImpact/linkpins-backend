using System.ComponentModel.DataAnnotations;

namespace BackSide2.BL.Models.BoardPinDto
{
    public class GetMainPagePinsDto
    {
        public int Offset { get; set; } = 0;
        public int Take { get; set; } = 15;
    }
}
