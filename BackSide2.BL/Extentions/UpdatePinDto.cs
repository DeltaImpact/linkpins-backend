using BackSide2.BL.Entity.BoardDto;
using BackSide2.BL.Entity.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class UpdatePinDtoExtentions
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