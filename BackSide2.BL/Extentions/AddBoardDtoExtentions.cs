using System;
using System.Collections.Generic;
using System.Text;
using BackSide2.BL.Entity;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class AddBoardDtoExtentions
    {
        public static Board toBoard(this AddBoardDto model, long personId)
        {
            var board = new Board()
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                IsPrivate = model.IsPrivate,
                CreatedBy = personId
            };
            board.UserId = personId;
            return board;
        }
    }
}