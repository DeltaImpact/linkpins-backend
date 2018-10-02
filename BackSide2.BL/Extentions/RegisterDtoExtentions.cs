using BackSide2.BL.Entity.PinDto;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class RegisterDtoExtentions
    {
        public static Person ToPerson(this RegisterDto model, Person createdBy)
        {
            var person = new Person
            {
                UserName = model.Username,
                Email = model.Email,
                Password = model.Password.GetPassHash(),
                Role = Role.User,
                FirstName = model.FirstName,
                Surname = model.Surname,
                Gender = null,
                Language = null,
                CreatedBy = createdBy.Id,
                UpdatedBy = null,
            };
            return person;
        }

    }
}