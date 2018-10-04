using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class AddPinDtoExtensions
    {
        public static Pin ToPin(this AddPinDto model, Person person)
        {
            var pin = new Pin
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                Link = model.Link,
                CreatedBy = person.Id
            };
            return pin;
        }
    }
}