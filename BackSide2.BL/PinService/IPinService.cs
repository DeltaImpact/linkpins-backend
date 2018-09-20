using System.Threading.Tasks;
using BackSide2.BL.Entity;

namespace BackSide2.BL.PinService
{
    public interface IPinService
    {
        Task<object> AddPinAsync(
            AddPinDto model, string userEmail
        );

        Task<object> DeletePinAsync(
            DeleteBoardDto model, string userEmail
        );

        Task<object> GetBoards(
            string userEmail
        );
    }
}