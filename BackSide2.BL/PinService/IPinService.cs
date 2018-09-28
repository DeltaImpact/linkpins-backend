using System.Threading.Tasks;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.PinDto;

namespace BackSide2.BL.PinService
{
    public interface IPinService
    {
        Task<object> AddPinAsync(AddPinDto model);

        Task<object> GetPinAsync(int pinId);
        Task<object> DeletePinAsync(DeletePinDto model);
        Task<object> UpdatePinAsync(UpdatePinDto model);
    }
}