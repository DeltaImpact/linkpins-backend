using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Entity
{
    public class AddBoardDto
    {
        [Required]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Debt name must be between 3 an 256")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        [Required] public bool IsPrivate { get; set; }
    }
}