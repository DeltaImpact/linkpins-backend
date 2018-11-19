using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.BL.Models.ProfileDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.BL.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Person> _personService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(
            IRepository<Person> personService, IHttpContextAccessor httpContextAccessor)
        {
            _personService = personService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProfileReturnDto> GetUserProfileInfo()
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = (await _personService.GetByIdAsync(userId)).ToProfileOwnReturnDto();
            return user;
        }

        public async Task<LoggedDto> ChangeProfileAsync(EditProfileDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userInDb = await _personService.GetByIdAsync(userId);

            var userNameExist =
                await (await _personService.GetAllAsync(user => user.UserName == model.Username && user.Id != userId))
                    .AnyAsync();
            if (userNameExist)
                throw new ObjectAlreadyExistException("Username already taken.");

            var emailExist =
                await (await _personService.GetAllAsync(user => user.Email == model.Email && user.Id != userId))
                    .AnyAsync();
            if (emailExist)
                throw new ObjectAlreadyExistException("Email already taken.");

            return (await _personService.UpdateAsync(model.ToPerson(userInDb))).ToLoggedDto();
        }

        public async Task ChangePasswordAsync(ChangePasswordDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var userInDb = await _personService.GetByIdAsync(userId);
            if (model.OldPassword.GetPassHash() != userInDb.Password)
                throw new Exception("Wrong password.");
            userInDb.Password = model.NewPassword.GetPassHash();
            await _personService.UpdateAsync(userInDb);
        }
    }
}