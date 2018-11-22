using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BackSide2.BL.Models.ChatDto
{
    public class GetPmDto
    {
        [Required] public string Message { get; set; }
        [Required] public long SentTo { get; set; }

    }
}