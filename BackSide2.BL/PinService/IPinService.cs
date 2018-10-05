using System.Threading.Tasks;
using BackSide2.BL.Models.PinDto;

namespace BackSide2.BL.PinService
{
    public interface IPinService
    {
        Task<object> AddPinAsync(AddPinDto model);

        Task<object> GetPinAsync(int pinId);
        Task<object> DeletePinAsync(int pinId);
        Task<object> UpdatePinAsync(UpdatePinDto model);
    }
}