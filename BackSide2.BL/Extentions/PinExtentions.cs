using System;
using System.Collections.Generic;
using System.Text;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.AuthorizeDto;
using BackSide2.BL.Entity.BoardDto;
using BackSide2.BL.Entity.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class PinExtentions
    {
        public static PinReturnDto ToPinReturnDto(this Pin pin, List<BoardReturnDto> boards = null)
        {
            return new PinReturnDto
            {
                Id = pin.Id,
                Name = pin.Name,
                Description = pin.Description,
                Img = pin.Img,
                Link = pin.Link,
                Modified = pin.Modified,
                Created = pin.Created,
                Boards = boards
            };
        }

        public static PinReturnDto ToPinReturnDto(this Pin pin)
        {
            return new PinReturnDto
            {
                Id = pin.Id,
                Name = pin.Name,
                Description = pin.Description,
                Img = pin.Img,
                Link = pin.Link,
                Modified = pin.Modified,
                Created = pin.Created,
            };
        }
    }
}