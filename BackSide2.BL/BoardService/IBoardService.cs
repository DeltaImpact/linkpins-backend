using System.Threading.Tasks;
using BackSide2.BL.Entity;

namespace BackSide2.BL.PinService
{
    public interface IBoardService
    {
        Task<object> AddBoardAsync(
            AddBoardDto model, string userEmail
        );

        Task<object> DeleteBoardAsync(
            DeleteBoardDto model, string userEmail
        );

        Task<object> GetBoardsAsync(
            string userEmail
        );
    }
}