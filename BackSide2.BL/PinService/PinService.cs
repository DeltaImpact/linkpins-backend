using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackSide2.BL.PinService
{
    public class PinService : IPinService
    {
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<Pin> _pinService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PinService(
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<Pin> pinService, IRepository<BoardPin> boardPinService,
            IHttpContextAccessor httpContextAccessor)
        {
            _boardService = boardService;
            _personService = personService;
            _pinService = pinService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<long> AddPinAsync(AddPinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();
            var boardInDb =
                await _boardService.GetByIdAsync(model.BoardId);
            if (boardInDb == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            if (boardInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to edit this board.");
            }

            var pin = await _pinService.InsertAsync(model.ToPin(usr));
            var relation = new BoardPin
            {
                CreatedBy = usr.Id,
                Pin = pin,
                Board = boardInDb
            };
            await _boardPinService.InsertAsync(relation);
            return pin.Id;
        }

        public async Task<PinReturnDto> GetPinAsync(int pinId)
        {
            //if (!_pinService.ExistsByIdAsync(pinId).Result)
            //{
            //    throw new ObjectNotFoundException("Pin not found.");
            //}

            var pin =
                await (await _pinService.GetAllAsync(d => d.Id == pinId, x => x.BoardPins))
                    .FirstOrDefaultAsync();

            if (pin == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            var boards =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId, x => x.Board))
                    .Select(e => e.Board.ToBoardReturnDto()).ToListAsync();

            var lastAction =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId, x => x.Board))
                    .Where(e => !e.Board.IsPrivate)
                    .Include(e => e.Board.Person)
                    .Select(e => e.ToLastPinActionDto())
                    .FirstOrDefaultAsync();

            return pin.ToPinReturnDto(boards, lastAction);
        }

        public async Task<PinReturnDto> DeletePinAsync(int pinId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pin =
                await _pinService.GetByIdAsync(pinId);

            if (pin == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            if (pin.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            var allPinConnections =
                await (await _boardPinService.GetAllAsync(d => d.Pin == pin, i => i.Board)).ToListAsync();


            foreach (var pinConnection in allPinConnections)
            {
                await _boardPinService.RemoveAsync(pinConnection);
            }

            var removedPin = await _pinService.RemoveAsync(pin);
            return removedPin.ToPinReturnDto();
        }

        public async Task<PinReturnDto> UpdatePinAsync(UpdatePinDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pinOld =
                await _pinService.GetByIdAsync(model.Id);
            if (pinOld == null)
            {
                throw new ObjectNotFoundException("Pin not found.");
            }

            if (pinOld.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            var board =
                await _pinService.UpdateAsync(model.ToPin(pinOld, userId));
            return board.ToPinReturnDto();
        }

        public async Task<List<PinReturnDto>> GetPageMain(int pageNumber, int elementsPerPage)
        {
            //if (pageNumber == null) pageNumber = 1;
            //if (elementsPerPage == null) elementsPerPage = 15;

            var pinsForPage = (await _pinService.GetAllAsync())
                .Include(e => e.BoardPins)
                .ThenInclude(e => e.Board)
                .Select(e => new
                {
                    Pin = e.ToPinReturnDto(),
                    IsPrivate = e.BoardPins.Select(i => i.Board).All(ii => ii.IsPrivate),
                })
                //.Where(e => e.IsPrivate == false)
                //.OrderBy(e => e.Pin.Created)
                .ToList();

            pinsForPage.RemoveAll(x => x.IsPrivate);
            return pinsForPage.Select(e => e.Pin).OrderByDescending(e => e.Created).ToList();


            //return PublicPins;
            //(await _pinService.GetAllAsync(pin => pin)).Include("Board").ToList();
            //.OrderBy(board => board.Created)
            //.Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
            //.ToList();


            //var boards = (await _boardService.GetAllAsync(board => person.Boards.Contains(board), board => board.BoardPins)).Skip(10).Take(20)
            //    .OrderBy(board => board.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins?.Count))
            //    .ToList();
            //throw new NotImplementedException();
        }
    }
}