using System.Threading.Tasks;
using BackSide2.BL.Models.ChatDto;
using BackSide2.BL.Models.PinDto;

namespace BackSide2.BL.ChatService
{
    public interface IChatService
    {
        Task<ReceiveMessageDto> SendMessageAsync(SendMessageDto Dto);
        Task<ReceiveMessageDto> GetLastMessagesInDialogAsync(int interlocutorId);


        Task<long> AddPinAsync(AddPinDto model);



        Task<PinReturnDto> GetPinAsync(int pinId);
        Task<PinReturnDto> DeletePinAsync(int pinId);
        Task<PinReturnDto> UpdatePinAsync(UpdatePinDto model);
    }
}