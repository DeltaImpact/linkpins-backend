using System.Collections.Generic;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class PinExtensions
    {

        public static PinReturnDto ToPinReturnDto(this Pin pin, List<BoardReturnDto> boards, LastPinActionDto lastPinAction)
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
                Boards = boards,
                LastAction = lastPinAction
            };
        }

        public static PinReturnDto ToPinReturnDto(this Pin pin, List<BoardReturnDto> boards)
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

        public static LastPinActionDto ToLastPinActionDto(this BoardPin boardPin)
        {
            return new LastPinActionDto
            {
                Date = boardPin.Created,
                UserName = boardPin.Board.Person.UserName,
                BoardName = boardPin.Board.Name,
                BoardId = boardPin.Board.Id,
                UserId = boardPin.Board.Person.Id
            };
        }
    }
}