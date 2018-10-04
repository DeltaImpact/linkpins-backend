using System.Threading.Tasks;
using BackSide2.BL.Models.BoardDto;

namespace BackSide2.BL.BoardService
{
    public interface IBoardService
    {
        Task<object> AddBoardAsync(
            AddBoardDto model
        );

        Task<object> DeleteBoardAsync(
            DeleteBoardDto model
        );

        Task<object> UpdateBoardAsync(
            UpdateBoardDto model
        );

        Task<object> GetBoardAsync(int boardId);

        Task<object> GetBoardsAsync();
 
    }
}