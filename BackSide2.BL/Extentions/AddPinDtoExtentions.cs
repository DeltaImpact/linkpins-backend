using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class AddPinDtoExtentions
    {
        //public static Board toPin(this AddPinDto model, long personId)
        //{
        //    var board = new Board()
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Img = model.Img,
        //        IsPrivate = model.IsPrivate,
        //        CreatedBy = personId
        //    };
        //    board.Person.Id = personId;
        //    return board;
        //}

        public static Pin ToPin(this AddPinDto model, Person person)
        {
            var pin = new Pin
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                Link = model.Link,
                //BoardId = model.BoardId,
                CreatedBy = person.Id
            };
            return pin;
        }

        //public static Board toPin(this AddPinDto model)
        //{
        //    var board = new Board()
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Img = model.Img,
        //        IsPrivate = model.IsPrivate,
        //    };
        //    //board.UserId = personId;
        //    return board;
        //}
    }
}