using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class LoggedDtoExtentions
    {
        public static LoggedDto ToLoggedDto(this Person person, string token)
        {
            return new LoggedDto
            {
                UserName = person.UserName,
                Email = person.Email,
                Role = person.Role.ToString(),
                Token = token,
            };
        }

        public static LoggedDto ToLoggedDto(this Person person)
        {
            return new LoggedDto
            {
                UserName = person.UserName,
                Email = person.Email,
                Role = person.Role.ToString(),
            };
        }
    }
}
