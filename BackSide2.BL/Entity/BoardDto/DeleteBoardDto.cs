using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Entity
{
    public class DeleteBoardDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Board name length name must be between 3 an 256")]
        public string Name { get; set; }
    }
}
