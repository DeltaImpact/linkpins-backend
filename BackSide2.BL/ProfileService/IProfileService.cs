using System.Threading.Tasks;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.BL.Models.ProfileDto;

namespace BackSide2.BL.ProfileService
{
    public interface IProfileService
    {
        Task<ProfileReturnDto> GetUserProfileInfo();
        Task<LoggedDto> ChangeProfileAsync(EditProfileDto model);
        Task ChangePasswordAsync(ChangePasswordDto model);
    }
}