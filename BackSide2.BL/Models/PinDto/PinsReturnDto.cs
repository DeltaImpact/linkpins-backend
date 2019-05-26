using System;
using System.Collections.Generic;
using BackSide2.BL.Models.BoardDto;

namespace BackSide2.BL.Models.PinDto
{
    public class PinsReturnDto
    {
        public List<PinReturnDto> Items { get; set; }
        public long Count { get; set; }
    }
}