using System;

namespace BackSide2.BL.Models.PinDto
{
    public class LastPinActionDto
    {
        public DateTime? Date { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string BoardName { get; set; }
        public long BoardId { get; set; }
    }
}