using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.BoardPinDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.BL.BoardPinService
{
    public class BoardPinService : IBoardPinService
    {
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<Pin> _pinService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardPinService(IRepository<Board> boardService,
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

        public async Task<List<PinReturnDto>> GetBoardPinsAsync(
            int boardId
        )
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var board =
                await _boardService.GetByIdAsync(boardId);

            if (board == null)
            {
                throw new PinServiceException("Board not found.");
            }

            if (board.CreatedBy != userId && board.IsPrivate)
            {
                throw new UnauthorizedAccessException();
            }

            var pins =
                await (await _boardPinService.GetAllAsync(d => d.Board.Id == boardId, x => x.Pin))
                    .Select(e => e.Pin.ToPinReturnDto())
                    .ToListAsync();

            return pins;
        }

        public async Task<List<BoardReturnDto>> GetBoardsWherePinsNotSavedAsync(
            int pinId
        )
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pin =
                await _pinService.GetByIdAsync(pinId);

            if (pin == null)
            {
                throw new PinServiceException("Pin not found.");
            }

            List<Board> boardsWherePinSaved =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId && d.CreatedBy == userId, x => x.Board)
                    ).Select(x => x.Board).ToListAsync();
            List<Board> boardsOfUser =
                (await _personService.GetAllAsync(d => d.Id == userId, x => x.Boards)).FirstOrDefault()?.Boards
                .ToList();
            if (boardsOfUser == null)
            {
                throw new Exception("Pin not found.");
            }

            var boardsWherePinNotSaved =
                boardsOfUser.Except(boardsWherePinSaved).Select(x => x.ToBoardReturnDto(true)).ToList();
            return boardsWherePinNotSaved;
        }

        public async Task<List<BoardReturnDto>> GetBoardsWherePinsSavedAsync(
            int pinId
        )
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pin =
                await _pinService.GetByIdAsync(pinId);

            if (pin == null)
            {
                throw new PinServiceException("Pin not found.");
            }

            var boards =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == pinId && d.CreatedBy == userId, x => x.Board)
                    )
                    .Select(e => e.Board.ToBoardReturnDto(true))
                    .ToListAsync();
            return boards;
        }


        public async Task<BoardReturnDto> AddPinToBoardAsync(AddPinToBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pinInDb =
                await _pinService.GetByIdAsync(model.PinId);
            if (pinInDb == null)
            {
                throw new BoardServiceException("Pin not found.");
            }

            Board boardInDb =
                await _boardService.GetByIdAsync(model.BoardId);
            if (boardInDb == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            if (boardInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to edit this board.");
            }

            BoardPin relation = new BoardPin
            {
                CreatedBy = userId,
                Pin = pinInDb,
                Board = boardInDb
            };
            await _boardPinService.InsertAsync(relation);

            return boardInDb.ToBoardReturnDto(true);
        }

        public async Task<BoardReturnDto> DeletePinFromBoardAsync(DeletePinFromBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var boardPinRelation =
                await (await _boardPinService.GetAllAsync(d => d.Pin.Id == model.PinId && d.Board.Id == model.BoardId,
                        x => x.Board))
                    .FirstOrDefaultAsync();

            if (boardPinRelation == null)
            {
                throw new BoardServiceException("Relation not found.");
            }

            if (boardPinRelation.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException();
            }

            await _boardPinService.RemoveAsync(boardPinRelation);

            return boardPinRelation.Board.ToBoardReturnDto(true);
        }
    }
}