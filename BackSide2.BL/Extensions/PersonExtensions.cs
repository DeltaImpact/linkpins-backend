using BackSide2.BL.Models.ProfileDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class PersonExtensions
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
