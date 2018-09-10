using System.Threading.Tasks;
using BackSide2.BL.Entity;

namespace BackSide2.BL.ParsePageService
{
    public interface IParsePageService
    {
        Task<object> ParsePageAsync(ParsePageDto entity);
    }
}