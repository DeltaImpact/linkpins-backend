using System.Collections.Generic;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class BoardExtensions
    {
        public static BoardReturnDto ToBoardReturnDto(this Board board)
        {
            return new BoardReturnDto
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                Img = board.Img,
                IsPrivate = board.IsPrivate,
                Modified = board.Modified,
                Created = board.Created,
            };
        }
        public static BoardReturnDto ToBoardReturnDto(this Board board, bool? isOwner)
        {
            return new BoardReturnDto
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                Img = board.Img,
                IsPrivate = board.IsPrivate,
                Modified = board.Modified,
                Created = board.Created,
                isOwner = isOwner
            };
        }
        public static BoardReturnDto ToBoardReturnDto(this Board board, List<PinReturnDto> pins, bool? isOwner)
        {
            return new BoardReturnDto
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                Img = board.Img,
                IsPrivate = board.IsPrivate,
                Modified = board.Modified,
                Created = board.Created,
                Pins = pins,
                isOwner = isOwner
            };
        }
    }
}