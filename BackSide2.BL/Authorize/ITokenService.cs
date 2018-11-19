using System.Threading.Tasks;
using BackSide2.BL.Models.AuthorizeDto;

namespace BackSide2.BL.Authorize
{
    public interface ITokenService 
    {
        Task<LoggedDto> RegisterAsync(RegisterDto entity);
        Task<LoggedDto> LoginAsync(LoginDto entity);
    }
}