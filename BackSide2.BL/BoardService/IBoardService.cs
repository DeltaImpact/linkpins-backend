using System.Collections.Generic;
using System.Threading.Tasks;
using BackSide2.BL.Models.BoardDto;

namespace BackSide2.BL.BoardService
{
    public interface IBoardService
    {
        Task<BoardReturnDto> AddBoardAsync(
            AddBoardDto model
        );

        Task<BoardReturnDto> DeleteBoardAsync(
            DeleteBoardDto model
        );

        Task<BoardReturnDto> UpdateBoardAsync(
            UpdateBoardDto model
        );

        Task<BoardReturnDto> GetBoardAsync(int boardId);

        Task<List<BoardReturnDto>> GetBoardsAsync();
 
    }
}