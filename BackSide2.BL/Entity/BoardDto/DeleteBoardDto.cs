using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Entity
{
    public class DeleteBoardDto
    {
        [Required]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Debt name must be between 3 an 256")]
        public string Name { get; set; }
    }
}
