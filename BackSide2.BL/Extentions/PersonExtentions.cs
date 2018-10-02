using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class PersonExtentions
    {
        public static ProfileReturnDto ToProfileOwnReturnDto(this Person person)
        {
            return new ProfileReturnDto
            {
                UserName = person.UserName,
                Email = person.Email,
                Role = person.Role.ToString(),
                FirstName = person.FirstName,
                Surname = person.Surname,
                Gender = person.Gender,
                Language = person.Language
            };
        }
    }
}
