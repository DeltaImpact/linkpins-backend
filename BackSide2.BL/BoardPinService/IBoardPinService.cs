using System.Collections.Generic;
using System.Threading.Tasks;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.BoardPinDto;
using BackSide2.BL.Models.PinDto;

namespace BackSide2.BL.BoardPinService
{
    public interface IBoardPinService
    {
        Task<List<PinReturnDto>> GetBoardPinsAsync(
            int boardId
        );

        Task<List<BoardReturnDto>> GetBoardsWherePinsNotSavedAsync(
            int pinId
        );

        Task<List<BoardReturnDto>> GetBoardsWherePinsSavedAsync(
            int pinId
        );

        Task<BoardReturnDto> AddPinToBoardAsync(AddPinToBoardDto model);

        Task<BoardReturnDto> DeletePinFromBoardAsync(DeletePinFromBoardDto model);
    }
}