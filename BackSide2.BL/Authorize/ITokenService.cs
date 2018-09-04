using System.Threading.Tasks;
using BackSide2.BL.Entity;

namespace BackSide2.BL.authorize
{
    public interface ITokenService 
    {
        Task<object> RegisterAsync(RegisterDto entity);
        Task<object> LoginAsync(LoginDto entity);
        Task<object> GetUserProfileInfo(string email);
    }
}