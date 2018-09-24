using System;
using System.Collections.Generic;
using System.Text;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.AuthorizeDto;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Extentions
{
    public static class PersonExtentions
    {
        public static ProfileOwnReturnDto toProfileOwnReturnDto(this Person person)
        {
            return new ProfileOwnReturnDto()
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
