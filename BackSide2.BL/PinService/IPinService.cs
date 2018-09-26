using System.Threading.Tasks;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.PinDto;

namespace BackSide2.BL.PinService
{
    public interface IPinService
    {
        Task<object> AddPinAsync(
            AddPinDto model, long personId
        );

        Task<object> GetPinAsync(
            int pinId,
            long personId
        );

    }
}