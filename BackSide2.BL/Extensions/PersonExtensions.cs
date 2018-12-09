using System.Threading.Tasks;
using BackSide2.BL.Models.ProfileDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extensions
{
    public static class PersonExtensions
    {
        public static ProfileReturnDto ToProfileOwnReturnDto(this Person person, bool isOnline)
        {
            return new ProfileReturnDto
            {
                Id = person.Id,
                UserName = person.UserName,
                Email = person.Email,
                Role = person.Role.ToString(),
                FirstName = person.FirstName,
                Surname = person.Surname,
                Gender = person.Gender,
                Language = person.Language,
                IsOnline = isOnline,
            };
        }

        public static ProfileReturnDto ToProfileReturnDto(this Person person)
        {
            return new ProfileReturnDto
            {
                Id = person.Id,
                UserName = person.UserName
            };
        }

        public static ProfileReturnDto ToProfileReturnDto(this Person person, bool isOnline)
        {
            return new ProfileReturnDto
            {
                Id = person.Id,
                UserName = person.UserName,
                IsOnline = isOnline,
            };
        }
    }
}