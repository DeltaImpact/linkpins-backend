using BackSide2.BL.Models.BoardDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class AddBoardDtoExtentions
    {
        public static Board ToBoard(this AddBoardDto model, long personId)
        {
            var board = new Board()
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                IsPrivate = model.IsPrivate,
                CreatedBy = personId
            };
            board.Person.Id = personId;
            return board;
        }

        public static Board ToBoard(this AddBoardDto model, Person person)
        {
            var board = new Board()
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                IsPrivate = model.IsPrivate,
                CreatedBy = person.Id
            };
            board.Person = person;
            return board;
        }

        public static Board ToBoard(this AddBoardDto model)
        {
            var board = new Board()
            {
                Name = model.Name,
                Description = model.Description,
                Img = model.Img,
                IsPrivate = model.IsPrivate,
            };
            //board.UserId = personId;
            return board;
        }
    }
}