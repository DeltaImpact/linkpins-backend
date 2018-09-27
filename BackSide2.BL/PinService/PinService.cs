using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Entity.PinDto;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extentions;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.PinService
{
    public class PinService : IPinService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<Pin> _pinService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PinService(
            IConfiguration configuration,
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<Pin> pinService, IRepository<BoardPin> boardPinService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _boardService = boardService;
            _personService = personService;
            _pinService = pinService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<object> AddPinAsync(AddPinDto model, long personId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == personId)).FirstOrDefaultAsync();

            Pin pinToAdd = model.ToPin(usr);

            var pin = await _pinService.InsertAsync(pinToAdd);
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Id == model.BoardId && d.Person.Id == personId))
                    .FirstOrDefaultAsync();

            BoardPin relation = new BoardPin
            {
                CreatedBy = usr.Id,
                Pin = pin,
                Board = boardInDb
            };

            var relatBoardPin = await _boardPinService.InsertAsync(relation);
            //var boards = (await _personService.GetAllAsync(d => d.Id == personId, x => x.Boards)).FirstOrDefault()?.Boards
            //    .Select(d => d.Id == model.BoardId && d.Person.Id == personId);
            //var boardPinInDb =
            //    (await (await _boardPinService.GetAllAsync(d => d.Id == relatBoardPin.Id, x => x.Board, y => y.Pin))
            //        .FirstOrDefaultAsync());
            //var resultpin = boardPinInDb.Pin.toPinReturnDto();
            return new { pin.Id };
            //throw new System.NotImplementedException();
        }

        public async Task<object> GetPinAsync(int pinId, long personId)
        {
            var pin =
                await(await _pinService.GetAllAsync(d => d.Id == pinId, x => x.BoardPins))
                    .FirstOrDefaultAsync();

            if (pin == null)
            {
                throw new BoardServiceException("Pin not found.");
            }

            var boards =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId, x => x.Board)).Select(e => e.Board.ToBoardReturnDto()).ToListAsync();


            return pin.ToPinReturnDto(boards);
        }
    }
}