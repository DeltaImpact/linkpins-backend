using System;
using System.Collections.Generic;
using BackSide2.BL.Models.PinDto;

namespace BackSide2.BL.Models.BoardDto
{
    public class BoardReturnDto
    {
        public bool? IsOwner;
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
        public List<PinReturnDto> Pins {get; set; }
        public bool? IsLast { get; set; }
    }
}