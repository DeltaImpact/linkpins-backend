using System.Threading.Tasks;
using BackSide2.BL.Entity;

namespace BackSide2.BL.PinService
{
    public interface IBoardService
    {
        Task<object> AddBoardAsync(
            AddBoardDto model, long userId
        );

        Task<object> DeleteBoardAsync(
            DeleteBoardDto model, long userId
        );

        Task<object> GetBoardsAsync(
            long userId
        );
    }
}