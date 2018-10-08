using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepository<Person> _personService;
        private readonly IRepository<BoardPin> _boardPinService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BoardService(
            IRepository<Board> boardService,
            IRepository<Person> personService,
            IRepository<BoardPin> boardPinService, IHttpContextAccessor httpContextAccessor)
        {
            _boardService = boardService;
            _personService = personService;
            _boardPinService = boardPinService;
            _httpContextAccessor = httpContextAccessor;
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
                throw new BoardServiceException("Board with such name already added.");
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
                throw new BoardServiceException("Board not found.");
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
                throw new BoardServiceException("Board not found.");
            }

            var boardWithSameName =
                await (await _boardService.GetAllAsync(d => d.Name == model.Name)).FirstOrDefaultAsync();
            if (boardWithSameName != null && model.Id != boardWithSameName.Id)
            {
                throw new BoardServiceException("Board with same name already exist.");
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
                throw new BoardServiceException("Board not found.");
            }

            if (board.CreatedBy != userId && board.IsPrivate)
            {
                throw new UnauthorizedAccessException();
            }

            var pins =
                await (await _boardPinService.GetAllAsync(d => d.Board.Id == boardId, x => x.Pin))
                    .Select(e => e.Pin.ToPinReturnDto())
                    .ToListAsync();

            var isOwner = board.Person.Id == userId;
            if (board == null)
            {
                throw new BoardServiceException("Board not found.");
            }

            return board.ToBoardReturnDto(pins, isOwner);
        }

        public async Task<List<BoardReturnDto>> GetBoardsAsync()
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boards = (await _personService.GetAllAsync(d => d.Id == userId, x => x.Boards)).FirstOrDefault()?.Boards
                .Select(o => o.ToBoardReturnDto()
                ).ToList();
            return boards;
        }
    }
}