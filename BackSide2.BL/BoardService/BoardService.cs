using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.BL.Extensions;
using BackSide2.BL.Models.BoardDto;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.BL.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly IRepository<Board> _boardService;
        private readonly IRepository<Pin> _pinService;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardService(
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<BoardPin> boardPinService, IHttpContextAccessor httpContextAccessor, IRepository<Pin> pinService)
        {
            _boardService = boardService;
            _personService = personService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
            _pinService = pinService;
        }


        public async Task<BoardReturnDto> AddBoardAsync(AddBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usr = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();
            var boardInDb =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name && d.Person.Id == userId))
                    .FirstOrDefaultAsync();
            if (boardInDb != null)
            {
                throw new ObjectAlreadyExistException("Board with such name already added.");
            }

            var boardToAdd = model.ToBoard(usr);

            var board = (await _boardService.InsertAsync(boardToAdd)).ToBoardReturnDto();

            return board;
        }

        public async Task<BoardReturnDto> DeleteBoardAsync(DeleteBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boardInDb =
                await (await _boardService.GetAllAsync(d => d.Id == model.Id && d.Person.Id == userId))
                    .FirstOrDefaultAsync();

            if (boardInDb == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            var board = (await _boardService.RemoveAsync(boardInDb)).ToBoardReturnDto();
            return board;
        }

        public async Task<BoardReturnDto> UpdateBoardAsync(UpdateBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boardOld =
                await (await _boardService.GetAllAsync(d => d.Id == model.Id)).FirstOrDefaultAsync();
            if (boardOld == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            var boardWithSameName =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name)).FirstOrDefaultAsync();
            if (boardWithSameName != null && model.Id != boardWithSameName.Id)
            {
                throw new ObjectAlreadyExistException("Board with same name already exist.");
            }

            var board =
                await _boardService.UpdateAsync(model.ToBoard(boardOld, userId));
            return board.ToBoardReturnDto();
        }

        public async Task<BoardReturnDto> GetBoardAsync(
            int boardId
        )
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var board =
                await (await _boardService.GetAllAsync(d => d.Id == boardId, x => x.Person)).FirstOrDefaultAsync();


            if (board == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            if (board.CreatedBy != userId && board.IsPrivate)
            {
                throw new UnauthorizedAccessException();
            }

            //var boardsPins =
            //    (await _pinService.GetAllAsync(pin => pin.BoardPins == userId, board => board.BoardPins))
            //    .OrderBy(board => board.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
            //    .ToList();

            var pins =
                await (await _boardPinService.GetAllAsync(d => d.Board.Id == boardId, x => x.Pin))
                    .Select(e => e.Pin.ToPinReturnDto())
                    .ToListAsync();

            var pinsCount =
                 (await _boardPinService.GetAllAsync(d => d.Board.Id == boardId, x => x.Pin))
                    .Count(); 


                //.Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
            var isOwner = board.Person.Id == userId;
            if (board == null)
            {
                throw new ObjectNotFoundException("Board not found.");
            }

            return board.ToBoardReturnDto(pins.ToPinsReturnDtoExtensions(pinsCount), isOwner);
        }

        public async Task<List<BoardReturnDto>> GetBoardsAsync()
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var personBoards =
            //    (await _boardService.GetAllAsync(board => board.Person.Id == userId, board => board.BoardPins))
            //    .OrderBy(board => board.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count))
            //    .ToList();

            var personBoards =
                (await _boardService.GetAllAsync(board => board.Person.Id == userId, board => board.BoardPins))
                .OrderBy(board => board.Created)
                .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
                .ToList();
            //var person = await _personService.GetByIdAsync(userId);
            //var personBoards1 = (await _boardService.GetAllAsync(board => person.Boards.Contains(board), board => board.BoardPins))
            //    .OrderBy(board => board.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count))
            //    .ToList();
            //var personBoards = (await _personService.GetAllAsync(d => d.Id == userId, x => x.Boards)).FirstOrDefault()?.Boards
            //    .OrderBy(ord => ord.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins?.Count))
            //    .ToList();
            return personBoards;
            //var boards = (await _personService.GetAllAsync(d => d.Id == userId, x => x.Boards)).FirstOrDefault()?.Boards
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins.Count))
            //    .ToList();
            //return boards;
        }

        public async Task<List<BoardReturnDto>> GetBoardsAsync(string userNickname)
        {
            if (userNickname == null)
            {
                return await GetBoardsAsync();
            }

            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user =
                await (await _personService.GetAllAsync(o => o.UserName == userNickname, x => x.Boards)).FirstAsync();
            if (user == null)
            {
                throw new ObjectNotFoundException("User not found.");
            }

            if (user.Id == userId)
            {
                return await GetBoardsAsync();
            }

            var personBoards =
                (await _boardService.GetAllAsync(board => board.Person.Id == user.Id))
                .Where(w => w.IsPrivate == false)
                .OrderBy(board => board.Created)
                .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, false))
                .ToList();

            //var personBoards =
            //    (await _boardService.GetAllAsync(board => board.Person.Id == userId, board => board.BoardPins))
            //    .Where(w => w.IsPrivate == false)
            //    .OrderBy(board => board.Created)
            //    .Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, false))
            //    .ToList();
            return personBoards;
        }
    }
}