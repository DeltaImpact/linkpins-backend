using BackSide2.BL.Entity.BoardDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class UpdateBoardDtoExtentions
    {
        public static Board ToBoard(this UpdateBoardDto model, Board board, long modifiedBy)
        {
            board.Name = model.Name;
            board.Description = model.Description;
            board.IsPrivate = model.IsPrivate;
            board.UpdatedBy = modifiedBy;
            return board;
        }
    }
}