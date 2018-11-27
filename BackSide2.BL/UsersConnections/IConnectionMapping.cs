using System.Threading.Tasks;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.BL.Models.ProfileDto;

namespace BackSide2.BL.UsersConnections
{
    public interface IConnectionMapping
    {
        Task Add(string connectionId);
        Task Remove(string connectionId);
        Task<bool> IsOnline(long userId);
    }
}