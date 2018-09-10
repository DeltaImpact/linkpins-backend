using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Entity
{
    public class ParsePageDto
    {
        [Required] public string Url { get; set; }
        public int MinTextLenght { get; set; } = 80;
    }
}
