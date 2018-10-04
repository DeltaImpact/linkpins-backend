using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class UpdatePinDtoExtensions
    {
        public static Pin ToPin(this UpdatePinDto model, Pin pin, long modifiedBy)
        {
            pin.Name = model.Name;
            pin.Description = model.Description;
            pin.UpdatedBy = modifiedBy;
            return pin;
        }
    }
}