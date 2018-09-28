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
            IRepository<Pin> pinService, IRepository<BoardPin> boardPinService,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _boardService = boardService;
            _personService = personService;
            _pinService = pinService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<object> AddPinAsync(AddPinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();

            Pin pinToAdd = model.ToPin(usr);

            var pin = await _pinService.InsertAsync(pinToAdd);
            Board boardInDb =
                await (await _boardService.GetAllAsync(d => d.Id == model.BoardId && d.Person.Id == userId))
                    .FirstOrDefaultAsync();

            BoardPin relation = new BoardPin
            {
                CreatedBy = usr.Id,
                Pin = pin,
                Board = boardInDb
            };

            return new {pin.Id};
        }

        public async Task<object> GetPinAsync(int pinId)
        {
            var pin =
                await (await _pinService.GetAllAsync(d => d.Id == pinId, x => x.BoardPins))
                    .FirstOrDefaultAsync();

            if (pin == null)
            {
                throw new BoardServiceException("Pin not found.");
            }

            var boards =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId, x => x.Board))
                    .Select(e => e.Board.ToBoardReturnDto()).ToListAsync();


            return pin.ToPinReturnDto(boards);
        }

        public async Task<object> DeletePinAsync(DeletePinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();

            var pin =
                await (await _pinService.GetAllAsync(d => d.Id == model.Id, x => x.BoardPins))
                    .FirstOrDefaultAsync();

            if (pin == null)
            {
                throw new BoardServiceException("Pin not found.");
            }

            var allPinConnections = await (await _boardPinService.GetAllAsync(d => d.Pin == pin)).ToListAsync();
            foreach (var pinConnection in allPinConnections)
            {
                var boardPin = (await _boardPinService.RemoveAsync(pinConnection));
            }

            var removedPin = (await _pinService.RemoveAsync(pin));
            return removedPin.ToPinReturnDto();
        }

        public async Task<object> UpdatePinAsync(UpdatePinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pinOld =
                await(await _pinService.GetAllAsync(d => d.Id == model.Id)).FirstOrDefaultAsync();
            if (pinOld == null)
            {
                throw new BoardServiceException("Pin not found.");
            }
            var board =
                await _pinService.UpdateAsync(model.ToPin(pinOld, userId));
            return new { board };
        }
    }
}