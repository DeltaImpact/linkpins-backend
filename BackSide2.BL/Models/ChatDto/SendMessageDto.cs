using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.BL.Models.ChatDto
{
    public class SendMessageDto
    {
        [Required]
        [StringLength(2000, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string MessageContent { get; set; }

        [Required] public long SentToId { get; set; }
    }
}