using System.Threading.Tasks;
using BackSide2.BL.Models.PinDto;

namespace BackSide2.BL.PinService
{
    public interface IPinService
    {
        Task<object> AddPinAsync(AddPinDto model);

        Task<PinReturnDto> GetPinAsync(int pinId);
        Task<PinReturnDto> DeletePinAsync(int pinId);
        Task<PinReturnDto> UpdatePinAsync(UpdatePinDto model);
    }
}