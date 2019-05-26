using System.Collections.Generic;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class PinsReturnDtoExtensions
    {
        public static PinsReturnDto ToPinsReturnDtoExtensions(this List<PinReturnDto> items, long count)
        {
            return new PinsReturnDto
            {
                Items = items,
                Count = count,
            };
        }
    }
}