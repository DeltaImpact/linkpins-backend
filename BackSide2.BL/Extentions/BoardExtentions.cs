using System;
using System.Collections.Generic;
using System.Text;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.AuthorizeDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class BoardExtentions
    {
        public static BoardReturnDto toBoardReturnDto(this Board board)
        {
            return new BoardReturnDto()
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
    }
}